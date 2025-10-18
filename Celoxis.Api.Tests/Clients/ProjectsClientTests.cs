using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Celoxis.Api.Exceptions;
using Celoxis.Api.Models;
using Celoxis.Api.Tests.Helpers;
using FluentAssertions;
using Flurl.Http.Testing;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Clients
{
    public class ProjectsClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public ProjectsClientTests()
        {
            _httpTest = new HttpTest();
            _client = new CeloxisClient("test-token", _baseUrl);
        }

        public void Dispose()
        {
            _httpTest.Dispose();
        }

        #region GetByIdAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_ValidId_ReturnsProject()
        {
            // Arrange
            var project = TestDataGenerator.GetProjectFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(project);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Projects.GetByIdAsync(project.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(project.Id);
            result.Name.Should().Be(project.Name);
            result.Budget.Should().Be(project.Budget);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/{project.Id}")
                .WithVerb(HttpMethod.Get)
                .WithHeader("Authorization", "bearer test-token")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_IncludesExpandParameter()
        {
            // Arrange
            var project = TestDataGenerator.GetProjectFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(project);
            var expand = new List<string> { "manager", "clients" };
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Projects.GetByIdAsync(project.Id, expand);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/{project.Id}")
                .WithQueryParam("expand", "manager,clients")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_NullId_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.GetByIdAsync("0"));
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_EmptyId_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.GetByIdAsync("-1"));
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_NotFound_ThrowsCeloxisApiException()
        {
            // Arrange
            _httpTest.RespondWith("Not found", 404);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Projects.GetByIdAsync("999"));
            
            exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
            exception.ResponseContent.Should().Contain("Not found");
        }

        #endregion

        #region GetByIdsAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetByIdsAsync_ValidIds_ReturnsProjects()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(3);
            var ids = projects.Select(p => p.Id).ToList();
            var response = TestDataGenerator.CreateApiResponse(projects);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Projects.GetByIdsAsync(ids);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Select(p => p.Id).Should().BeEquivalentTo(ids);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/{string.Join(",", ids)}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdsAsync_MoreThan10Ids_ThrowsArgumentException()
        {
            // Arrange
            var ids = Enumerable.Range(1, 11).Select(id => id.ToString()).ToList();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.GetByIdsAsync(ids));
            
            exception.Message.Should().Contain("Cannot query more than 10 IDs at a time");
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdsAsync_NullIds_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.GetByIdsAsync(null!));
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdsAsync_EmptyIds_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.GetByIdsAsync([]));
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_NoParameters_ReturnsAllProjects()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(5);
            var response = TestDataGenerator.CreateApiResponse(projects, 5);
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, nextPage) = await _client.Projects.QueryAsync();

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(5);
            totalRecords.Should().Be(5);
            nextPage.Should().BeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_WithFilter_IncludesFilterParameter()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(2);
            var response = TestDataGenerator.CreateApiResponse(projects, 2);
            var query = new QueryBuilder()
                .Where("state", States.Project.Active)
                .Where("budget", ">50000")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Projects.QueryAsync(query);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithQueryParam("filter")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_WithSort_IncludesSortParameter()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(projects, 3);
            var query = new QueryBuilder()
                .OrderBy("plannedStart")
                .OrderBy("budget", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Projects.QueryAsync(query);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithQueryParam("sort", "plannedStart,budget/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_WithPagination_IncludesPageParameter()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(10);
            var response = TestDataGenerator.CreateApiResponse(projects, 50, 3);
            var query = new QueryBuilder().Page(2).Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, nextPage) = await _client.Projects.QueryAsync(query);

            // Assert
            data.Should().HaveCount(10);
            totalRecords.Should().Be(50);
            nextPage.Should().Be(3);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithQueryParam("page", "2")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_ComplexQuery_IncludesAllParameters()
        {
            // Arrange
            var projects = TestDataGenerator.GetProjectFaker().Generate(5);
            var response = TestDataGenerator.CreateApiResponse(projects, 25, 2);
            var query = new QueryBuilder()
                .Where("state", States.Project.Active)
                .WhereIn("type", "Infrastructure", "Implementation")
                .OrderBy("budget", descending: true)
                .Expand("manager", "clients")
                .Page(1)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Projects.QueryAsync(query);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "budget/desc")
                .WithQueryParam("expand", "manager,clients")
                .WithQueryParam("page", "1")
                .Times(1);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidData_ReturnsCreatedProject()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateProjectRequestFaker().Generate();
            var project = TestDataGenerator.GetProjectFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(project);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Projects.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(project.Id);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithVerb(HttpMethod.Post)
                .WithContentType("application/json")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_NullData_ThrowsArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => 
                _client.Projects.CreateAsync(null!));
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidationError_ThrowsCeloxisApiException()
        {
            // Arrange
            _httpTest.RespondWith("Validation error: Name is required", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Projects.CreateAsync(new { }));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region CreateBatchAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateBatchAsync_ValidData_ReturnsCreatedProjects()
        {
            // Arrange
            var createRequests = TestDataGenerator.GetCreateProjectRequestFaker().Generate(3);
            var projects = TestDataGenerator.GetProjectFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(projects);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Projects.CreateBatchAsync(createRequests.Cast<object>().ToList());

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateBatchAsync_EmptyList_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.CreateBatchAsync(new List<CreateProjectRequest>().Cast<object>().ToList()));
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ValidData_ReturnsUpdatedProject()
        {
            var updateRequest = new UpdateProjectRequest { Id = "1234", Budget = 75000m };
            var project = TestDataGenerator.GetProjectFaker().Generate();
            project.Budget = 75000m;
            var response = TestDataGenerator.CreateSingleResponse(project);
            
            _httpTest.RespondWithJson(response);

            var result = await _client.Projects.UpdateAsync(updateRequest);

            result.Should().NotBeNull();
            result.Budget.Should().Be(75000m);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects")
                .WithVerb("PATCH")
                .Times(1);
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        [UnitTest]
        public async Task DeleteAsync_ValidId_CallsDeleteEndpoint()
        {
            // Arrange
            _httpTest.RespondWith(status: 204);

            // Act
            await _client.Projects.DeleteAsync("1234");

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/1234")
                .WithVerb(HttpMethod.Delete)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task DeleteAsync_EmptyId_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _client.Projects.DeleteAsync("0"));
        }

        #endregion

        #region CloneAsync Tests

        [Fact]
        [UnitTest]
        public async Task CloneAsync_ValidId_ReturnsClonedProject()
        {
            // Arrange
            var overrideData = new 
            { 
                name = "Cloned Project", 
                plannedStart = DateTime.Now 
            };
            var project = TestDataGenerator.GetProjectFaker().Generate();
            project.Name = "Cloned Project";
            var response = TestDataGenerator.CreateSingleResponse(project);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Projects.CloneAsync("1234", overrideData);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Cloned Project");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/1234/clone")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CloneAsync_NoOverrideData_SendsEmptyObject()
        {
            // Arrange
            var project = TestDataGenerator.GetProjectFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(project);
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Projects.CloneAsync("1234");

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/projects/1234/clone")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(new { })
                .Times(1);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task AnyOperation_RateLimitExceeded_ThrowsCeloxisRateLimitException()
        {
            // Arrange
            _httpTest.RespondWith("Rate limit exceeded", 429);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisRateLimitException>(() => 
                _client.Projects.GetByIdAsync("1234"));
            
            exception.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
            exception.Message.Should().Contain("API rate limit exceeded");
        }

        [Fact]
        [UnitTest]
        public async Task AnyOperation_ServerError_ThrowsCeloxisApiException()
        {
            // Arrange
            _httpTest.RespondWith("Internal server error", 500);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Projects.GetByIdAsync("1234"));
            
            exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        #endregion
    }
}
