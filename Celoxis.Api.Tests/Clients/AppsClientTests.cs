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
    public class AppsClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public AppsClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsApp()
        {
            // Arrange
            var app = TestDataGenerator.GetAppFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(app);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.GetByIdAsync(app.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(app.Id);
            result.Name.Should().Be(app.Name);
            result.App.Should().Be(app.App);
            result.State.Should().Be(app.State);
            result.Priority.Should().Be(app.Priority);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps/{app.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_ExpandsAssignee()
        {
            // Arrange
            var app = TestDataGenerator.GetAppFaker().Generate();
            var user = TestDataGenerator.GetUserFaker().Generate();
            app.Assignee = user.Username;
            
            var response = TestDataGenerator.CreateSingleResponse(app);
            var expand = new List<string> { "assignee", "project" };
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.GetByIdAsync(app.Id, expand);

            // Assert
            result.Assignee.Should().NotBeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps/{app.Id}")
                .WithQueryParam("expand", "assignee,project")
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByAppType_ReturnsBugs()
        {
            // Arrange
            var apps = TestDataGenerator.GetAppFaker()
                .RuleFor(a => a.App, "Bug")
                .Generate(5);
            var response = TestDataGenerator.CreateApiResponse(apps, 5);
            var query = new QueryBuilder()
                .Where("app", "Bug")
                .OrderBy("created", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.Apps.QueryAsync(query);

            // Assert
            data.Should().HaveCount(5);
            data.Should().OnlyContain(a => a.App == "Bug");
            totalRecords.Should().Be(5);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "created/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByState_ReturnsOpenApps()
        {
            // Arrange
            var apps = TestDataGenerator.GetAppFaker()
                .RuleFor(a => a.Open, true)
                .RuleFor(a => a.State, f => f.PickRandom("Reported", "In Progress"))
                .Generate(7);
            var response = TestDataGenerator.CreateApiResponse(apps, 7);
            var query = new QueryBuilder()
                .Where("open", "Yes")
                .WhereIn("state", "Reported", "In Progress")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Apps.QueryAsync(query);

            // Assert
            data.Should().HaveCount(7);
            data.Should().OnlyContain(a => a.Open == true);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByPriority_ReturnsHighPriorityApps()
        {
            // Arrange
            var apps = TestDataGenerator.GetAppFaker()
                .RuleFor(a => a.Priority, f => f.PickRandom(Priorities.VeryHigh, Priorities.High))
                .Generate(3);
            var response = TestDataGenerator.CreateApiResponse(apps, 3);
            var query = new QueryBuilder()
                .WhereIn("priority", Priorities.VeryHigh, Priorities.High)
                .Where("open", "Yes")
                .OrderBy("priority")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Apps.QueryAsync(query);

            // Assert
            data.Should().HaveCount(3);
            data.Should().OnlyContain(a => a.Priority == Priorities.VeryHigh || a.Priority == Priorities.High);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByCustomFields_ReturnsMatchingApps()
        {
            // Arrange
            var apps = TestDataGenerator.GetAppFaker().Generate(4);
            var response = TestDataGenerator.CreateApiResponse(apps, 4);
            var query = new QueryBuilder()
                .Where("app", "Bug")
                .Where("custom_bug_severity", "Major")
                .WhereIn("custom_bug_component", "Component 1", "Component 2")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Apps.QueryAsync(query);

            // Assert
            data.Should().HaveCount(4);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps")
                .WithQueryParam("filter")
                .Times(1);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_CreateBug_CreatesSuccessfully()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateAppRequestFaker()
                .RuleFor(r => r.Name, "API Login Bug")
                .RuleFor(r => r.Description, "Users cannot login via API when using special characters")
                .RuleFor(r => r.App, "Bug")
                .RuleFor(r => r.State, "Reported")
                .RuleFor(r => r.Priority, Priorities.High)
                .RuleFor(r => r.Assignee, "john.developer")
                .RuleFor(r => r.Project, "API-PROJECT")
                .RuleFor(r => r.DueDate, DateTime.Today.AddDays(3))
                .Generate();
            
            createRequest.OtherFields["custom_bug_type"] = "Bug";
            createRequest.OtherFields["custom_bug_severity"] = "Major";
            createRequest.OtherFields["custom_bug_component"] = "Authentication";
            
            var app = TestDataGenerator.GetAppFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(app);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_CreateRisk_CreatesWithRiskFields()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateAppRequestFaker()
                .RuleFor(r => r.Name, "Data Loss Risk")
                .RuleFor(r => r.Description, "Risk of data loss during migration")
                .RuleFor(r => r.App, "Risk")
                .RuleFor(r => r.State, "Reported")
                .RuleFor(r => r.Priority, Priorities.VeryHigh)
                .RuleFor(r => r.Project, "MIGRATION-PROJECT")
                .Generate();
            
            createRequest.OtherFields["custom_risk_impact"] = "High";
            createRequest.OtherFields["custom_risk_probability"] = "Medium";
            createRequest.OtherFields["custom_risk_potential_cost"] = 50000;
            createRequest.OtherFields["custom_risk_mitigation_cost"] = 5000;
            createRequest.OtherFields["custom_mitigation_plan"] = "Implement backup procedures";
            
            var app = TestDataGenerator.GetAppFaker().Generate();
            app.App = "Risk";
            var response = TestDataGenerator.CreateSingleResponse(app);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.App.Should().Be("Risk");
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeState_UpdatesSuccessfully()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateAppRequestFaker()
                .RuleFor(r => r.Id, "12345")
                .RuleFor(r => r.State, "In Progress")
                .RuleFor(r => r.Assignee, "jane.developer")
                .RuleFor(r => r.LastUpdate, "Started working on the issue")
                .Generate();
            
            var app = TestDataGenerator.GetAppFaker().Generate();
            app.State = "In Progress";
            var response = TestDataGenerator.CreateSingleResponse(app);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.State.Should().Be("In Progress");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps")
                .WithVerb("PATCH")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ResolveBug_UpdatesWithResolution()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateAppRequestFaker()
                .RuleFor(r => r.Id, "12345")
                .RuleFor(r => r.State, "Resolved")
                .RuleFor(r => r.LastUpdate, "Fixed the authentication issue")
                .Generate();
            
            updateRequest.OtherFields["custom_bug_resolution"] = "Fixed";
            updateRequest.OtherFields["custom_bug_fix_version"] = "2.1";
            
            var app = TestDataGenerator.GetAppFaker().Generate();
            app.State = "Resolved";
            var response = TestDataGenerator.CreateSingleResponse(app);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Apps.UpdateAsync(updateRequest);

            // Assert
            result.State.Should().Be("Resolved");
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task BugTracking_CreateAndUpdateBugLifecycle_Success()
        {
            // Simulate full bug lifecycle
            // Arrange
            var createBugRequest = TestDataGenerator.GetCreateAppRequestFaker()
                .RuleFor(r => r.Name, "Critical Production Bug")
                .RuleFor(r => r.Description, "Application crashes on startup")
                .RuleFor(r => r.App, "Bug")
                .RuleFor(r => r.State, "Reported")
                .RuleFor(r => r.Priority, Priorities.VeryHigh)
                .Generate();
            
            createBugRequest.OtherFields["custom_bug_severity"] = "Critical";
            createBugRequest.OtherFields["custom_bug_component"] = "Core";
            
            var bug = TestDataGenerator.GetAppFaker().Generate();
            bug.Id = "99999";
            
            var updateRequests = new UpdateAppRequest[]
            {
                TestDataGenerator.GetUpdateAppRequestFaker().RuleFor(r => r.Id, "99999").RuleFor(r => r.State, "In Progress").RuleFor(r => r.Assignee, "dev.team").Generate(),
                TestDataGenerator.GetUpdateAppRequestFaker().RuleFor(r => r.Id, "99999").RuleFor(r => r.State, "Testing").Generate(),
                TestDataGenerator.GetUpdateAppRequestFaker().RuleFor(r => r.Id, "99999").RuleFor(r => r.State, "Resolved").Generate()
            };
            
            updateRequests[1].OtherFields["custom_bug_fix_version"] = "1.0.1";
            updateRequests[2].OtherFields["custom_bug_resolution"] = "Fixed";
            
            _httpTest
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(bug))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(bug))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(bug))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(bug));

            // Act
            var createdBug = await _client.Apps.CreateAsync(createBugRequest);
            
            foreach (var update in updateRequests)
            {
                await _client.Apps.UpdateAsync(update);
            }

            // Assert
            createdBug.Should().NotBeNull();
        }

        [Fact]
        [IntegrationTest]
        public async Task RiskManagement_QueryHighRisks_Success()
        {
            // Simulate risk management query
            // Arrange
            var risks = TestDataGenerator.GetAppFaker()
                .RuleFor(a => a.App, "Risk")
                .RuleFor(a => a.Priority, f => f.PickRandom(Priorities.VeryHigh, Priorities.High))
                .RuleFor(a => a.Open, true)
                .Generate(8);
            var response = TestDataGenerator.CreateApiResponse(risks, 8);
            
            var query = new QueryBuilder()
                .Where("app", "Risk")
                .WhereIn("priority", Priorities.VeryHigh, Priorities.High)
                .Where("open", "Yes")
                .Expand("project", "assignee")
                .OrderBy("priority")
                .OrderBy("created", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Apps.QueryAsync(query);

            // Assert
            data.Should().HaveCount(8);
            data.Should().OnlyContain(r => r.App == "Risk");
            data.Should().OnlyContain(r => r.Priority == Priorities.VeryHigh || r.Priority == Priorities.High);
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        [UnitTest]
        public async Task DeleteAsync_ValidId_DeletesSuccessfully()
        {
            // Arrange
            _httpTest.RespondWith(status: 204);

            // Act
            await _client.Apps.DeleteAsync("12345");

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/apps/12345")
                .WithVerb(HttpMethod.Delete)
                .Times(1);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_MissingRequiredFields_ThrowsValidationError()
        {
            // Arrange
            var createRequest = new
            {
                name = "Incomplete App"
                // Missing required fields like app, state
            };
            
            _httpTest.RespondWith("Validation error: 'app' is required", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Apps.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ResponseContent.Should().Contain("'app' is required");
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ClosedApp_ThrowsError()
        {
            // Arrange
            var updateRequest = new { id = "12345", state = "In Progress" };
            
            _httpTest.RespondWith("Cannot modify closed app", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Apps.UpdateAsync(updateRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        #endregion
    }
}
