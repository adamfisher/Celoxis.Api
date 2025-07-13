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
    public class TasksClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public TasksClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsTask()
        {
            // Arrange
            var task = TestDataGenerator.GetTaskFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(task);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.GetByIdAsync(task.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(task.Id);
            result.Name.Should().Be(task.Name);
            result.PlannedEffort.Should().Be(task.PlannedEffort);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/{task.Id}")
                .WithVerb(HttpMethod.Get)
                .WithHeader("Authorization", "bearer test-token")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_IncludesExpandParameter()
        {
            // Arrange
            var task = TestDataGenerator.GetTaskFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(task);
            var expand = new List<string> { "project", "parent", "assignments" };
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Tasks.GetByIdAsync(task.Id, expand);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/{task.Id}")
                .WithQueryParam("expand", "project,parent,assignments")
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_WithComplexFilter_ReturnsFilteredTasks()
        {
            // Arrange
            var tasks = TestDataGenerator.GetTaskFaker().Generate(5);
            var response = TestDataGenerator.CreateApiResponse(tasks, 5);
            var query = new QueryBuilder()
                .WhereStartsWith("name", "API")
                .Where("project.state", States.Project.Active)
                .Where("actualPercentComplete", ">50")
                .OrderBy("plannedStart")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, nextPage) = await _client.Tasks.QueryAsync(query);

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(5);
            totalRecords.Should().Be(5);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "plannedStart")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByMultipleValues_UsesArrayFilter()
        {
            // Arrange
            var tasks = TestDataGenerator.GetTaskFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(tasks, 3);
            var query = new QueryBuilder()
                .WhereIn("scheduleHealth", "On Track", "At Risk")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            await _client.Tasks.QueryAsync(query);

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks")
                .WithQueryParam("filter")
                .Times(1);
        }

        #endregion

        #region GetPredecessorsAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetPredecessorsAsync_ValidTaskId_ReturnsPredecessors()
        {
            // Arrange
            var predecessors = TestDataGenerator.GetTaskFaker().Generate(2);
            var response = TestDataGenerator.CreateApiResponse(predecessors);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.GetPredecessorsAsync(12345);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/12345/predecessors")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetPredecessorsAsync_TaskWithNoPredecessors_ReturnsEmptyList()
        {
            // Arrange
            var response = TestDataGenerator.CreateApiResponse(new List<CeloxisTask>());
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.GetPredecessorsAsync(12345);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        #endregion

        #region GetSuccessorsAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetSuccessorsAsync_ValidTaskId_ReturnsSuccessors()
        {
            // Arrange
            var successors = TestDataGenerator.GetTaskFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(successors);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.GetSuccessorsAsync(12345);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/12345/successors")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region GetAssignmentsAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetAssignmentsAsync_ValidTaskId_ReturnsAssignments()
        {
            // Arrange
            var assignments = new List<object>
            {
                new { id = "1", user = "John Doe", allocation = 100 },
                new { id = "2", user = "Jane Smith", allocation = 50 }
            };
            var response = TestDataGenerator.CreateApiResponse(assignments);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.GetAssignmentsAsync(12345);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/12345/assignments")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_WithResourceAssignments_CreatesTaskSuccessfully()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTaskRequestFaker()
                .RuleFor(r => r.Resources, "Joe Cool[50%], Peter Parker")
                .Generate();
            
            var task = TestDataGenerator.GetTaskFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(task);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(task.Id);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_WithParentTask_CreatesSubtask()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTaskRequestFaker()
                .RuleFor(r => r.Parent, "98765")
                .Generate();
            
            var task = TestDataGenerator.GetTaskFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(task);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_UpdatePercentComplete_ReturnsUpdatedTask()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateTaskRequestFaker()
                .RuleFor(r => r.Id, "12345")
                .RuleFor(r => r.ActualPercentComplete, 75)
                .RuleFor(r => r.RemainingEffort, "10")
                .Generate();
            
            var task = TestDataGenerator.GetTaskFaker().Generate();
            task.ActualPercentComplete = "75";
            task.RemainingEffort = "10";
            var response = TestDataGenerator.CreateSingleResponse(task);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.ActualPercentComplete.Should().Be("75");
            result.RemainingEffort.Should().Be("10");
        }

        #endregion

        #region CloneAsync Tests

        [Fact]
        [UnitTest]
        public async Task CloneAsync_WithNewProject_ClonesTaskToNewProject()
        {
            // Arrange
            var overrideData = TestDataGenerator.GetUpdateTaskRequestFaker()
                .RuleFor(r => r.Name, "Cloned Task")
                .RuleFor(r => r.PlannedStart, DateTime.Now.AddDays(7))
                .Generate();
            
            var task = TestDataGenerator.GetTaskFaker().Generate();
            task.Name = "Cloned Task";
            var response = TestDataGenerator.CreateSingleResponse(task);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Tasks.CloneAsync("12345", overrideData);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Cloned Task");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/tasks/12345/clone")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_InvalidProjectCode_ThrowsCeloxisApiException()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTaskRequestFaker()
                .RuleFor(r => r.Project, "INVALID")
                .RuleFor(r => r.Name, "Test Task")
                .Generate();
            
            _httpTest.RespondWith("CeloxisProject not found", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Tasks.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ResponseContent.Should().Contain("CeloxisProject not found");
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_TaskLocked_ThrowsCeloxisApiException()
        {
            // Arrange
            _httpTest.RespondWith("Task is locked", 423);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Tasks.UpdateAsync(TestDataGenerator.GetUpdateTaskRequestFaker().RuleFor(r => r.Id, "12345").RuleFor(r => r.Name, "New Name").Generate()));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Locked);
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task ComplexWorkflow_CreateTaskWithPredecessors_Success()
        {
            // This test simulates creating a task with dependencies
            // Arrange
            var task1 = TestDataGenerator.GetTaskFaker().Generate();
            var task2 = TestDataGenerator.GetTaskFaker().Generate();
            
            _httpTest
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(task1))
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(task2));

            // Act
            var firstTask = await _client.Tasks.CreateAsync(TestDataGenerator.GetCreateTaskRequestFaker()
                .RuleFor(r => r.Project, "TEST-PROJ")
                .RuleFor(r => r.Name, "First Task")
                .RuleFor(r => r.PlannedEffort, "16")
                .RuleFor(r => r.Duration, "2d")
                .Generate());

            var secondTask = await _client.Tasks.CreateAsync(TestDataGenerator.GetCreateTaskRequestFaker()
                .RuleFor(r => r.Project, "TEST-PROJ")
                .RuleFor(r => r.Name, "Second Task")
                .RuleFor(r => r.PlannedEffort, "8")
                .RuleFor(r => r.Duration, "1d")
                .Generate());

            // Assert
            firstTask.Should().NotBeNull();
            secondTask.Should().NotBeNull();
            
            _httpTest.CallLog.Should().HaveCount(2);
        }

        #endregion
    }
}
