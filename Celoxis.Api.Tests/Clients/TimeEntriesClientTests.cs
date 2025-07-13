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
    public class TimeEntriesClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public TimeEntriesClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsTimeEntry()
        {
            // Arrange
            var timeEntry = TestDataGenerator.GetTimeEntryFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(timeEntry);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.GetByIdAsync(timeEntry.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(timeEntry.Id);
            result.Hours.Should().Be(timeEntry.Hours);
            result.Date.Should().Be(timeEntry.Date);
            result.TimeCode.Should().Be(timeEntry.TimeCode);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries/{timeEntry.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_ExpandsRelatedEntities()
        {
            // Arrange
            var timeEntry = TestDataGenerator.GetTimeEntryFaker().Generate();
            var user = TestDataGenerator.GetUserFaker().Generate();
            var project = TestDataGenerator.GetProjectFaker().Generate();
            
            // Simulate expanded response
            timeEntry.User = user;
            timeEntry.Project = project;
            
            var response = TestDataGenerator.CreateSingleResponse(timeEntry);
            var expand = new List<string> { "user", "project", "workItem" };
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.GetByIdAsync(timeEntry.Id, expand);

            // Assert
            result.Should().NotBeNull();
            result.User.Should().NotBeNull();
            result.Project.Should().NotBeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries/{timeEntry.Id}")
                .WithQueryParam("expand", "user,project,workItem")
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByDateRange_ReturnsTimeEntriesInRange()
        {
            // Arrange
            var timeEntries = TestDataGenerator.GetTimeEntryFaker().Generate(10);
            var response = TestDataGenerator.CreateApiResponse(timeEntries, 10);
            var query = new QueryBuilder()
                .Where("date", DateFilters.ThisWeek)
                .OrderBy("date", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, nextPage) = await _client.TimeEntries.QueryAsync(query);

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(10);
            totalRecords.Should().Be(10);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "date/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByUser_ReturnsUserTimeEntries()
        {
            // Arrange
            var timeEntries = TestDataGenerator.GetTimeEntryFaker().Generate(5);
            var response = TestDataGenerator.CreateApiResponse(timeEntries, 5);
            var query = new QueryBuilder()
                .Where("user.name", "John Doe")
                .Where("state", States.TimeEntry.Approved)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.TimeEntries.QueryAsync(query);

            // Assert
            data.Should().HaveCount(5);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries")
                .WithQueryParam("filter")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByBillable_ReturnsBillableEntries()
        {
            // Arrange
            var timeEntries = TestDataGenerator.GetTimeEntryFaker()
                .RuleFor(e => e.IsBillable, "Yes")
                .Generate(7);
            var response = TestDataGenerator.CreateApiResponse(timeEntries, 7);
            var query = new QueryBuilder()
                .Where("isBillable", "Yes")
                .Where("date", DateFilters.LastMonth)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.TimeEntries.QueryAsync(query);

            // Assert
            data.Should().HaveCount(7);
            data.Should().OnlyContain(e => e.IsBillable == "Yes");
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidTimeEntry_CreatesSuccessfully()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTimeEntryRequestFaker()
                .RuleFor(r => r.User, "john.doe")
                .RuleFor(r => r.WorkItem, "12345")
                .RuleFor(r => r.Date, DateTime.Today)
                .RuleFor(r => r.Hours, 6.5m)
                .RuleFor(r => r.TimeCode, "Development")
                .RuleFor(r => r.Comments, "Worked on API integration")
                .RuleFor(r => r.State, States.TimeEntry.Saved)
                .Generate();
            
            var timeEntry = TestDataGenerator.GetTimeEntryFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(timeEntry);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_FutureDate_ThrowsValidationError()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTimeEntryRequestFaker()
                .RuleFor(r => r.User, "john.doe")
                .RuleFor(r => r.WorkItem, "12345")
                .RuleFor(r => r.Date, DateTime.Today.AddDays(7))
                .RuleFor(r => r.Hours, 8m)
                .Generate();
            
            _httpTest.RespondWith("Cannot create time entry for future date", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TimeEntries.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        #endregion

        #region CreateBatchAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateBatchAsync_MultipleEntries_CreatesAllSuccessfully()
        {
            // Arrange
            var createRequests = new List<object>
            {
                TestDataGenerator.GetCreateTimeEntryRequestFaker()
                    .RuleFor(r => r.User, "john.doe")
                    .RuleFor(r => r.WorkItem, "12345")
                    .RuleFor(r => r.Date, DateTime.Today)
                    .RuleFor(r => r.Hours, 4m)
                    .RuleFor(r => r.TimeCode, "Development")
                    .Generate(),
                TestDataGenerator.GetCreateTimeEntryRequestFaker()
                    .RuleFor(r => r.User, "john.doe")
                    .RuleFor(r => r.WorkItem, "12346")
                    .RuleFor(r => r.Date, DateTime.Today)
                    .RuleFor(r => r.Hours, 4m)
                    .RuleFor(r => r.TimeCode, "Testing")
                    .Generate()
            };
            
            var timeEntries = TestDataGenerator.GetTimeEntryFaker().Generate(2);
            var response = TestDataGenerator.CreateApiResponse(timeEntries);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.CreateBatchAsync(createRequests);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeHoursAndComments_UpdatesSuccessfully()
        {
            // Arrange
            var updateRequest = new
            {
                id = "234567",
                hours = 7.5m,
                comments = "Updated: Added extra time for debugging"
            };
            
            var timeEntry = TestDataGenerator.GetTimeEntryFaker().Generate();
            timeEntry.Hours = 7.5m;
            timeEntry.Comments = "Updated: Added extra time for debugging";
            var response = TestDataGenerator.CreateSingleResponse(timeEntry);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.Hours.Should().Be(7.5m);
            result.Comments.Should().Contain("Updated");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/timeEntries")
                .WithVerb("PATCH")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeStateToSubmitted_UpdatesState()
        {
            // Arrange
            var updateRequest = new
            {
                id = "234567",
                state = States.TimeEntry.PendingApproval
            };
            
            var timeEntry = TestDataGenerator.GetTimeEntryFaker().Generate();
            timeEntry.State = States.TimeEntry.PendingApproval;
            var response = TestDataGenerator.CreateSingleResponse(timeEntry);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.UpdateAsync(updateRequest);

            // Assert
            result.State.Should().Be(States.TimeEntry.PendingApproval);
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task WeeklyTimesheet_CreateMultipleEntriesForWeek_Success()
        {
            // Simulate creating a week's worth of time entries
            // Arrange
            var monday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            var entries = new List<object>();
            
            for (int i = 0; i < 5; i++)
            {
                entries.Add(new CreateTimeEntryRequest
                {
                    User = "john.doe",
                    WorkItem = "12345",
                    Date = monday.AddDays(i),
                    Hours = 8m,
                    TimeCode = "Development",
                    Comments = $"Day {i + 1} work"
                });
            }
            
            var timeEntries = TestDataGenerator.GetTimeEntryFaker().Generate(5);
            // Set hours to match the request
            foreach (var entry in timeEntries)
            {
                entry.Hours = 8m;
            }
            var response = TestDataGenerator.CreateApiResponse(timeEntries);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.TimeEntries.CreateBatchAsync(entries);

            // Assert
            result.Should().HaveCount(5);
            result.Sum(e => e.Hours).Should().Be(40m); // Full work week
        }

        [Fact]
        [IntegrationTest]
        public async Task MonthlyReport_QueryAndCalculateTotals_Success()
        {
            // Simulate generating a monthly report
            // Arrange
            var timeEntries = TestDataGenerator.GetTimeEntryFaker().Generate(20);
            var response = TestDataGenerator.CreateApiResponse(timeEntries, 20);
            var query = new QueryBuilder()
                .Where("date", DateFilters.LastMonth)
                .Where("user.id", "123456")
                .Expand("project", "workItem")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.TimeEntries.QueryAsync(query);

            // Assert
            data.Should().HaveCount(20);
            
            // Calculate totals
            var totalHours = data.Sum(e => e.Hours);
            var billableHours = data.Where(e => e.IsBillable == "Yes").Sum(e => e.Hours);
            var totalRevenue = data.Sum(e => e.Revenue);
            var totalCost = data.Sum(e => e.Cost);
            
            totalHours.Should().BeGreaterThan(0);
            billableHours.Should().BeGreaterThan(0);
            totalRevenue.Should().BeGreaterThan(0);
            totalCost.Should().BeGreaterThan(0);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ExceedsHoursLimit_ThrowsValidationError()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateTimeEntryRequestFaker()
                .RuleFor(r => r.User, "john.doe")
                .RuleFor(r => r.WorkItem, "12345")
                .RuleFor(r => r.Date, DateTime.Today)
                .RuleFor(r => r.Hours, 25m) // More than 24 hours
                .Generate();
            
            _httpTest.RespondWith("Hours cannot exceed 24 for a single day", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TimeEntries.CreateAsync(createRequest));
            
            exception.ResponseContent.Should().Contain("Hours cannot exceed 24");
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ApprovedEntry_ThrowsError()
        {
            // Arrange
            var updateRequest = new { id = "234567", hours = 8m };
            
            _httpTest.RespondWith("Cannot modify approved time entry", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.TimeEntries.UpdateAsync(updateRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        #endregion
    }
}
