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
    public class TaskUpdatesClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public TaskUpdatesClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsTaskUpdate()
        {
            // Arrange
            var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(taskUpdate);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.GetByIdAsync(taskUpdate.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(taskUpdate.Id);
            result.Update.Should().Be(taskUpdate.Update);
            result.CreatedBy.Should().Be(taskUpdate.CreatedBy);
            result.Created.Should().Be(taskUpdate.Created);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates/{taskUpdate.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_ExpandsTask()
        {
            // Arrange
            var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
            var task = TestDataGenerator.GetTaskFaker().Generate();
            taskUpdate.Task = task;
            
            var response = TestDataGenerator.CreateSingleResponse(taskUpdate);
            var expand = new List<string> { "task" };
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.GetByIdAsync(taskUpdate.Id, expand);

            // Assert
            result.Should().NotBeNull();
            result.Task.Should().NotBeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates/{taskUpdate.Id}")
                .WithQueryParam("expand", "task")
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByTask_ReturnsTaskUpdates()
        {
            // Arrange
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker().Generate(5);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates, 5);
            var query = new QueryBuilder()
                .Where("task.id", "12345")
                .OrderBy("created", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.TaskUpdates.QueryAsync(query);

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(5);
            totalRecords.Should().Be(5);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "created/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByCreatedBy_ReturnsUserUpdates()
        {
            // Arrange
            var userName = "John Developer";
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker()
                .RuleFor(t => t.CreatedBy, userName)
                .Generate(7);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates, 7);
            var query = new QueryBuilder()
                .Where("createdBy", userName)
                .Where("created", DateFilters.ThisWeek)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.TaskUpdates.QueryAsync(query);

            // Assert
            data.Should().HaveCount(7);
            data.Should().OnlyContain(u => u.CreatedBy == userName);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByDateRange_ReturnsRecentUpdates()
        {
            // Arrange
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker().Generate(10);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates, 10);
            var query = new QueryBuilder()
                .Where("created", DateFilters.LastNDays(7))
                .Expand("task")
                .OrderBy("created", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.TaskUpdates.QueryAsync(query);

            // Assert
            data.Should().HaveCount(10);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates")
                .WithQueryParam("filter")
                .WithQueryParam("expand", "task")
                .WithQueryParam("sort", "created/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_WithKeywordSearch_ReturnsMatchingUpdates()
        {
            // Arrange
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker()
                .RuleFor(t => t.Update, f => $"Fixed bug #{f.Random.Number(100, 999)}")
                .Generate(3);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates, 3);
            var query = new QueryBuilder()
                .Where("update", "~bug")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.TaskUpdates.QueryAsync(query);

            // Assert
            data.Should().HaveCount(3);
            data.Should().OnlyContain(u => u.Update.Contains("bug", StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidUpdate_CreatesSuccessfully()
        {
            // Arrange
            var createRequest = new
            {
                task = "12345",
                update = "Completed the API integration. All endpoints are now functional and tested.",
                createdBy = "john.developer"
            };
            
            var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(taskUpdate);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();

            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_StatusUpdate_CreatesWithStatusInfo()
        {
            // Arrange
            var createRequest = new
            {
                task = "12345",
                update = "Status Update: Task is 75% complete. Expecting to finish by end of week.",
                createdBy = "project.manager",
                percentComplete = 75
            };
            
            var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
            taskUpdate.Update = "Status Update: Task is 75% complete. Expecting to finish by end of week.";
            var response = TestDataGenerator.CreateSingleResponse(taskUpdate);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Update.Should().Contain("Status Update");
        }

        #endregion

        #region CreateBatchAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateBatchAsync_MultipleUpdates_CreatesAllSuccessfully()
        {
            // Arrange
            var createRequests = new List<object>
            {
                new
                {
                    task = "12345",
                    update = "Started working on the feature",
                    createdBy = "dev.team"
                },
                new
                {
                    task = "12346",
                    update = "Code review completed. Minor changes requested.",
                    createdBy = "dev.team"
                },
                new
                {
                    task = "12347",
                    update = "Deployed to staging environment for testing",
                    createdBy = "dev.team"
                }
            };
            
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.CreateBatchAsync(createRequests);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_EditUpdate_UpdatesSuccessfully()
        {
            // Arrange
            var updateRequest = new
            {
                id = "999888",
                update = "EDITED: Completed the API integration. All endpoints are now functional and tested. Added unit tests."
            };
            
            var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
            taskUpdate.Update = "EDITED: Completed the API integration. All endpoints are now functional and tested. Added unit tests.";
            var response = TestDataGenerator.CreateSingleResponse(taskUpdate);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.Update.Should().StartWith("EDITED:");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates")
                .WithVerb("PATCH")
                .Times(1);
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
            await _client.TaskUpdates.DeleteAsync("999888");

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/taskUpdates/999888")
                .WithVerb(HttpMethod.Delete)
                .Times(1);
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task TaskProgress_CreateProgressUpdates_Success()
        {
            // Simulate creating progress updates for a task
            // Arrange
            var taskId = "12345";
            var progressUpdates = new[]
            {
                new { task = taskId, update = "Started development", percentComplete = 0 },
                new { task = taskId, update = "Completed initial implementation", percentComplete = 25 },
                new { task = taskId, update = "Added unit tests", percentComplete = 50 },
                new { task = taskId, update = "Code review in progress", percentComplete = 75 },
                new { task = taskId, update = "Deployed to production", percentComplete = 100 }
            };
            
            foreach (var _ in progressUpdates)
            {
                var taskUpdate = TestDataGenerator.GetTaskUpdateFaker().Generate();
                _httpTest.RespondWithJson(TestDataGenerator.CreateSingleResponse(taskUpdate));
            }

            // Act
            var createdUpdates = new List<UpdateTaskRequest>();
            foreach (var update in progressUpdates)
            {
                var created = await _client.TaskUpdates.CreateAsync(update);

                createdUpdates.Add(new UpdateTaskRequest
                {
                    Id = created.Id,
                    Name = null, 
                    Description = null,
                });
            }

            // Assert
            createdUpdates.Should().HaveCount(5);
            _httpTest.CallLog.Should().HaveCount(5);
        }

        [Fact]
        [IntegrationTest]
        public async Task ProjectStatusReport_GetAllRecentUpdates_Success()
        {
            // Simulate getting all recent updates for a project status report
            // Arrange
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker().Generate(25);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates, 25);
            
            var query = new QueryBuilder()
                .Where("task.project.id", "PROJECT-123")
                .Where("created", DateFilters.LastNDays(7))
                .Expand("task")
                .OrderBy("created", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.TaskUpdates.QueryAsync(query);

            // Assert
            data.Should().HaveCount(25);
            totalRecords.Should().Be(25);
            
            // Group updates by task
            var updatesByTask = data.GroupBy(u => u.Task)
                .Select(g => new { Task = g.Key, UpdateCount = g.Count() })
                .ToList();
            
            updatesByTask.Should().NotBeEmpty();
        }

        [Fact]
        [IntegrationTest]
        public async Task DailyStandup_CreateTeamUpdates_Success()
        {
            // Simulate daily standup updates from team members
            // Arrange
            var teamMembers = new[] { "john.dev", "jane.dev", "bob.dev" };
            var tasks = new[] { "12345", "12346", "12347" };
            var createRequests = new List<object>();
            
            for (int i = 0; i < teamMembers.Length; i++)
            {
                createRequests.Add(new
                {
                    task = tasks[i],
                    update = $"Daily Update: Working on {tasks[i]}. Progress is on track.",
                    createdBy = teamMembers[i]
                });
            }
            
            var taskUpdates = TestDataGenerator.GetTaskUpdateFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(taskUpdates);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TaskUpdates.CreateBatchAsync(createRequests);

            // Assert
            result.Should().HaveCount(3);
            
            // Verify each team member created an update
            var updateCreators = result.Select(u => u.CreatedBy).Distinct().ToList();
            updateCreators.Should().HaveCount(3);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_EmptyUpdate_ThrowsValidationError()
        {
            // Arrange
            var createRequest = new
            {
                task = "12345",
                update = "",
                createdBy = "john.dev"
            };
            
            _httpTest.RespondWith("Update text cannot be empty", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TaskUpdates.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ResponseContent.Should().Contain("Update text cannot be empty");
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_InvalidTaskId_ThrowsNotFoundError()
        {
            // Arrange
            var createRequest = new
            {
                task = "99999999",
                update = "This will fail",
                createdBy = "john.dev"
            };
            
            _httpTest.RespondWith("Task not found", 404);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TaskUpdates.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_OtherUsersUpdate_ThrowsForbiddenError()
        {
            // Arrange
            var updateRequest = new
            {
                id = "999888",
                update = "Trying to edit someone else's update"
            };
            
            _httpTest.RespondWith("Cannot modify other user's updates", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TaskUpdates.UpdateAsync(updateRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        [UnitTest]
        public async Task DeleteAsync_OldUpdate_ThrowsError()
        {
            // Arrange
            _httpTest.RespondWith("Cannot delete updates older than 24 hours", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TaskUpdates.DeleteAsync("999888"));
            
            exception.ResponseContent.Should().Contain("older than 24 hours");
        }

        #endregion
    }
}
