using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Celoxis.Api.Exceptions;
using Celoxis.Api.Tests.Helpers;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests
{
    public class CeloxisClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public CeloxisClientTests()
        {
            _httpTest = new HttpTest();
        }

        public void Dispose()
        {
            _httpTest.Dispose();
        }

        #region Constructor Tests

        [Fact]
        [UnitTest]
        public void Constructor_ValidToken_CreatesClient()
        {
            // Act
            var client = new CeloxisClient("valid-token");

            // Assert
            client.Should().NotBeNull();
            client.Projects.Should().NotBeNull();
            client.Tasks.Should().NotBeNull();
            client.TimeEntries.Should().NotBeNull();
            client.Apps.Should().NotBeNull();
            client.Users.Should().NotBeNull();
            client.Expenses.Should().NotBeNull();
            client.TaskUpdates.Should().NotBeNull();
        }

        [Fact]
        [UnitTest]
        public void Constructor_CustomBaseUrl_UsesProvidedUrl()
        {
            // Arrange
            var customUrl = "https://custom.celoxis.com/psa";

            // Act
            var client = new CeloxisClient("valid-token", customUrl);

            // Assert
            client.Should().NotBeNull();
        }

        [Fact]
        [UnitTest]
        public void Constructor_NullToken_ThrowsArgumentException()
        {
            // Act & Assert
            Action act = () => new CeloxisClient(null!);
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Access token cannot be null or empty*");
        }

        [Fact]
        [UnitTest]
        public void Constructor_EmptyToken_ThrowsArgumentException()
        {
            // Act & Assert
            Action act = () => new CeloxisClient(string.Empty);
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Access token cannot be null or empty*");
        }

        [Fact]
        [UnitTest]
        public void Constructor_WhitespaceToken_ThrowsArgumentException()
        {
            // Act & Assert
            Action act = () => new CeloxisClient("   ");
            act.Should().Throw<ArgumentException>()
                .WithMessage("*Access token cannot be null or empty*");
        }

        #endregion

        #region Authentication Tests

        [Fact]
        [UnitTest]
        public async Task Request_IncludesAuthorizationHeader()
        {
            var token = "test-auth-token";
            var client = new CeloxisClient(token, _baseUrl);
            
            var project = TestDataGenerator.GetProjectFaker().RuleFor(p => p.Id, "123").Generate();
            _httpTest.RespondWithJson(TestDataGenerator.CreateSingleResponse(project));

            await client.Projects.GetByIdAsync("123");

            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/123")
                .WithHeader("Authorization", $"bearer {token}")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task Request_IncludesContentTypeHeader()
        {
            var client = new CeloxisClient("test-token", _baseUrl);
            
            var project = TestDataGenerator.GetProjectFaker().RuleFor(p => p.Id, "123").Generate();
            _httpTest.RespondWithJson(TestDataGenerator.CreateSingleResponse(project));

            await client.Projects.GetByIdAsync("123");

            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/123")
                .WithHeader("Content-Type", "application/json")
                .Times(1);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task Request_401Unauthorized_ThrowsCeloxisApiException()
        {
            // Arrange
            var client = new CeloxisClient("invalid-token", _baseUrl);
            
            _httpTest.RespondWith("Unauthorized", 401);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                client.Projects.GetByIdAsync("123"));

            exception.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            exception.Message.Should().Contain("API request failed");
            exception.ResponseContent.Should().Be("Unauthorized");
        }

        [Fact]
        [UnitTest]
        public async Task Request_429RateLimitExceeded_ThrowsCeloxisRateLimitException()
        {
            // Arrange
            var client = new CeloxisClient("test-token", _baseUrl);
            
            _httpTest.RespondWith("Rate limit exceeded", 429);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisRateLimitException>(() => 
                client.Projects.GetByIdAsync("123"));

            exception.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
            exception.Message.Should().Contain("API rate limit exceeded");
        }

        [Fact]
        [UnitTest]
        public async Task Request_500ServerError_ThrowsCeloxisApiException()
        {
            // Arrange
            var client = new CeloxisClient("test-token", _baseUrl);
            
            _httpTest.RespondWith("Internal server error", 500);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                client.Projects.GetByIdAsync("123"));

            exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        [UnitTest]
        public async Task Request_NetworkError_ThrowsException()
        {
            // Arrange
            var client = new CeloxisClient("test-token", _baseUrl);
            
            _httpTest.SimulateTimeout();

            // Act & Assert
            await Assert.ThrowsAnyAsync<Exception>(() => 
                client.Projects.GetByIdAsync("123"));
        }

        #endregion

        #region URL Building Tests

        [Fact]
        [UnitTest]
        public async Task Request_WithQueryParameters_BuildsCorrectUrl()
        {
            // Arrange
            var client = new CeloxisClient("test-token", _baseUrl);
            var query = new QueryBuilder()
                .Where("state", "Active")
                .Where("budget", ">50000")
                .OrderBy("plannedStart", descending: true)
                .Expand("manager", "clients")
                .Page(2)
                .Build();
            
            _httpTest.RespondWithJson(new { data = new List<object>(), totalRecords = 0 });

            // Act
            await client.Projects.QueryAsync(query);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "plannedStart/desc")
                .WithQueryParam("expand", "manager,clients")
                .WithQueryParam("page", "2")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task Request_EmptyQueryParameters_NoQueryString()
        {
            // Arrange
            var client = new CeloxisClient("test-token", _baseUrl);
            
            _httpTest.RespondWithJson(new { data = new List<object>(), totalRecords = 0 });

            // Act
            await client.Projects.QueryAsync();

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithoutQueryParam("filter")
                .WithoutQueryParam("sort")
                .WithoutQueryParam("expand")
                .WithoutQueryParam("page")
                .Times(1);
        }

        #endregion

        #region Integration Tests

        [Fact]
        [IntegrationTest]
        public async Task FullWorkflow_CreateUpdateDelete_Success()
        {
            var client = new CeloxisClient("test-token", _baseUrl);
            
            var createdProject = TestDataGenerator.GetProjectFaker()
                .RuleFor(p => p.Id, "999")
                .RuleFor(p => p.Name, "Test Project")
                .RuleFor(p => p.State, "Active")
                .Generate();
                
            var updatedProject = TestDataGenerator.GetProjectFaker()
                .RuleFor(p => p.Id, "999")
                .RuleFor(p => p.Name, "Updated Project")
                .RuleFor(p => p.State, "Active")
                .Generate();
            
            _httpTest
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(createdProject))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(updatedProject))
                .RespondWith(status: 204);

            var createRequest = TestDataGenerator.GetCreateProjectRequestFaker()
                .RuleFor(r => r.Name, "Test Project")
                .RuleFor(r => r.Manager, "John Manager")
                .RuleFor(r => r.PlannedStart, DateTime.Now)
                .RuleFor(r => r.State, "Active")
                .RuleFor(r => r.Type, "Implementation")
                .RuleFor(r => r.Workspace, "Default")
                .Generate();
                
            var created = await client.Projects.CreateAsync(createRequest);

            var updateRequest = TestDataGenerator.GetUpdateProjectRequestFaker()
                .RuleFor(r => r.Id, created.Id)
                .RuleFor(r => r.Name, "Updated Project")
                .Generate();
                
            var updated = await client.Projects.UpdateAsync(updateRequest);

            await client.Projects.DeleteAsync(updated.Id);

            created.Should().NotBeNull();
            created.Id.Should().Be("999");
            created.Name.Should().Be("Test Project");

            updated.Should().NotBeNull();
            updated.Name.Should().Be("Updated Project");
        }

        [Fact]
        [IntegrationTest]
        public async Task ParallelRequests_MultipleClients_Success()
        {
            var client1 = new CeloxisClient("token1", _baseUrl);
            var client2 = new CeloxisClient("token2", _baseUrl);
            
            var project1 = TestDataGenerator.GetProjectFaker().RuleFor(p => p.Id, 1.ToString()).RuleFor(p => p.Name, "Project 1").Generate();
            var project2 = TestDataGenerator.GetProjectFaker().RuleFor(p => p.Id, 2.ToString()).RuleFor(p => p.Name, "Project 2").Generate();
            
            _httpTest
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(project1))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(project2));

            var task1 = client1.Projects.GetByIdAsync("1");
            var task2 = client2.Projects.GetByIdAsync("2");
            var results = await Task.WhenAll(task1, task2);

            results.Should().HaveCount(2);
            results[0].Id.Should().Be("1");
            results[1].Id.Should().Be("2");

            _httpTest.ShouldHaveCalled("*")
                .WithHeader("Authorization", "bearer token1")
                .Times(1);
            
            _httpTest.ShouldHaveCalled("*")
                .WithHeader("Authorization", "bearer token2")
                .Times(1);
        }

        #endregion

        #region Custom Flurl Client Tests

        [Fact]
        [UnitTest]
        public void Constructor_WithCustomFlurlClient_UsesProvidedClient()
        {
            var mockFlurlClient = new FlurlClient();

            var client = new CeloxisClient("test-token", _baseUrl, mockFlurlClient);

            client.Should().NotBeNull();
        }

        #endregion
    }
}
