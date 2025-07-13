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
    public class ExpensesClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public ExpensesClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsExpense()
        {
            // Arrange
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(expense);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.GetByIdAsync(expense.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(expense.Id);
            result.Name.Should().Be(expense.Name);
            result.Amount.Should().Be(expense.Amount);
            result.Category.Should().Be(expense.Category);
            result.Date.Should().Be(expense.Date);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses/{expense.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_WithExpand_ExpandsRelatedEntities()
        {
            // Arrange
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            var project = TestDataGenerator.GetProjectFaker().Generate();
            var user = TestDataGenerator.GetUserFaker().Generate();
            
            expense.Project = project;
            expense.User = user;
            
            var response = TestDataGenerator.CreateSingleResponse(expense);
            var expand = new List<string> { "project", "task", "user" };
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.GetByIdAsync(expense.Id, expand);

            // Assert
            result.Should().NotBeNull();
            result.Project.Should().NotBeNull();
            result.User.Should().NotBeNull();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses/{expense.Id}")
                .WithQueryParam("expand", "project,task,user")
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByDateRange_ReturnsExpensesInRange()
        {
            // Arrange
            var expenses = TestDataGenerator.GetExpenseFaker().Generate(15);
            var response = TestDataGenerator.CreateApiResponse(expenses, 15);
            var query = new QueryBuilder()
                .Where("date", DateFilters.LastMonth)
                .OrderBy("date", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(15);
            totalRecords.Should().Be(15);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "date/desc")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByCategory_ReturnsExpensesByCategory()
        {
            // Arrange
            var expenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.Category, "Travel")
                .Generate(8);
            var response = TestDataGenerator.CreateApiResponse(expenses, 8);
            var query = new QueryBuilder()
                .Where("category", "Travel")
                .Where("amount", ">100")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().HaveCount(8);
            data.Should().OnlyContain(e => e.Category == "Travel");
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByState_ReturnsPendingExpenses()
        {
            // Arrange
            var expenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.State, "Submitted")
                .Generate(5);
            var response = TestDataGenerator.CreateApiResponse(expenses, 5);
            var query = new QueryBuilder()
                .Where("state", "Submitted")
                .Expand("user", "project")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().HaveCount(5);
            data.Should().OnlyContain(e => e.State == "Submitted");
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByProject_ReturnsProjectExpenses()
        {
            // Arrange
            var expenses = TestDataGenerator.GetExpenseFaker().Generate(12);
            var response = TestDataGenerator.CreateApiResponse(expenses, 12);
            var query = new QueryBuilder()
                .Where("project.id", "12345")
                .OrderBy("amount", descending: true)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().HaveCount(12);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "amount/desc")
                .Times(1);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidExpense_CreatesSuccessfully()
        {
            // Arrange
            var createRequest = new
            {
                name = "Flight to Client Site",
                description = "Round trip flight for client meeting",
                date = DateTime.Today.AddDays(-2),
                amount = 650.50m,
                category = "Travel",
                project = "CLIENT-PROJECT",
                task = "12345",
                user = "john.doe",
                state = "Submitted"
            };
            
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(expense);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ExpenseWithReceipt_CreatesWithAttachment()
        {
            // Arrange
            var createRequest = new
            {
                name = "Office Supplies",
                date = DateTime.Today,
                amount = 125.99m,
                category = "Equipment",
                project = "INTERNAL",
                description = "Monitors and keyboards for new hires",
                receiptNumber = "REC-2024-001"
            };
            
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(expense);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
        }

        #endregion

        #region CreateBatchAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateBatchAsync_MultipleExpenses_CreatesAllSuccessfully()
        {
            // Arrange
            var createRequests = new List<object>
            {
                new
                {
                    name = "Hotel Stay",
                    date = DateTime.Today.AddDays(-3),
                    amount = 350m,
                    category = "Travel",
                    project = "CLIENT-PROJECT"
                },
                new
                {
                    name = "Client Dinner",
                    date = DateTime.Today.AddDays(-2),
                    amount = 175m,
                    category = "Meals",
                    project = "CLIENT-PROJECT"
                },
                new
                {
                    name = "Taxi to Airport",
                    date = DateTime.Today.AddDays(-1),
                    amount = 45m,
                    category = "Travel",
                    project = "CLIENT-PROJECT"
                }
            };
            
            var expenses = TestDataGenerator.GetExpenseFaker().Generate(3);
            var response = TestDataGenerator.CreateApiResponse(expenses);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.CreateBatchAsync(createRequests);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeAmount_UpdatesSuccessfully()
        {
            // Arrange
            var updateRequest = new
            {
                id = "98765",
                amount = 725.50m,
                description = "Updated: Added airport parking fees"
            };
            
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            expense.Amount = 725.50m;
            expense.Description = "Updated: Added airport parking fees";
            var response = TestDataGenerator.CreateSingleResponse(expense);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.Amount.Should().Be(725.50m);
            result.Description.Should().Contain("Updated");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses")
                .WithVerb("PATCH")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_SubmitForApproval_ChangesState()
        {
            // Arrange
            var updateRequest = new
            {
                id = "98765",
                state = "Submitted"
            };
            
            var expense = TestDataGenerator.GetExpenseFaker().Generate();
            expense.State = "Submitted";
            var response = TestDataGenerator.CreateSingleResponse(expense);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Expenses.UpdateAsync(updateRequest);

            // Assert
            result.State.Should().Be("Submitted");
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
            await _client.Expenses.DeleteAsync("98765");

            // Assert
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/expenses/98765")
                .WithVerb(HttpMethod.Delete)
                .Times(1);
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task ExpenseReport_CreateMonthlyExpenseReport_Success()
        {
            // Simulate creating a monthly expense report
            // Arrange
            var travelExpenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.Category, "Travel")
                .Generate(5);
            var mealExpenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.Category, "Meals")
                .Generate(3);
            var otherExpenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.Category, f => f.PickRandom("Equipment", "Software", "Other"))
                .Generate(2);
            
            var allExpenses = travelExpenses.Concat(mealExpenses).Concat(otherExpenses).ToList();
            var response = TestDataGenerator.CreateApiResponse(allExpenses, 10);
            
            var query = new QueryBuilder()
                .Where("date", DateFilters.LastMonth)
                .Where("user.id", "123456")
                .Expand("project", "task")
                .OrderBy("date")
                .OrderBy("category")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().HaveCount(10);
            
            // Calculate totals by category
            var totalsByCategory = data.GroupBy(e => e.Category)
                .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToList();
            
            totalsByCategory.Should().NotBeEmpty();
            totalsByCategory.Should().Contain(t => t.Category == "Travel");
            totalsByCategory.Should().Contain(t => t.Category == "Meals");
            
            var grandTotal = data.Sum(e => e.Amount);
            grandTotal.Should().BeGreaterThan(0);
        }

        [Fact]
        [IntegrationTest]
        public async Task ExpenseApproval_BulkApprovalWorkflow_Success()
        {
            // Simulate bulk approval of expenses
            // Arrange
            var pendingExpenses = TestDataGenerator.GetExpenseFaker()
                .RuleFor(e => e.State, "Submitted")
                .Generate(5);
            var queryResponse = TestDataGenerator.CreateApiResponse(pendingExpenses, 5);
            
            var query = new QueryBuilder()
                .Where("state", "Submitted")
                .Where("date", DateFilters.ThisMonth)
                .Build();
            
            _httpTest.RespondWithJson(queryResponse);
            
            // Setup update responses
            foreach (var expense in pendingExpenses)
            {
                var approvedExpense = TestDataGenerator.GetExpenseFaker().Generate();
                approvedExpense.State = "Approved";
                _httpTest.RespondWithJson(TestDataGenerator.CreateSingleResponse(approvedExpense));
            }

            // Act
            var (pendingData, _, _) = await _client.Expenses.QueryAsync(query);
            
            var approvalTasks = pendingData.Select(expense => 
                _client.Expenses.UpdateAsync(new { id = expense.Id, state = "Approved" })
            );
            
            var approvedExpenses = await Task.WhenAll(approvalTasks);

            // Assert
            pendingData.Should().HaveCount(5);
            approvedExpenses.Should().HaveCount(5);
            approvedExpenses.Should().OnlyContain(e => e.State == "Approved");

            _httpTest.CallLog.Should().HaveCount(6); // 1 query + 5 updates
        }

        [Fact]
        [IntegrationTest]
        public async Task ProjectExpenses_CalculateProjectCosts_Success()
        {
            // Simulate calculating total project expenses
            // Arrange
            var projectExpenses = TestDataGenerator.GetExpenseFaker().Generate(20);
            var response = TestDataGenerator.CreateApiResponse(projectExpenses, 20);
            
            var query = new QueryBuilder()
                .Where("project.code", "PROJ-2024-001")
                .Where("state", "Approved")
                .OrderBy("date")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Expenses.QueryAsync(query);

            // Assert
            data.Should().HaveCount(20);
            
            // Calculate project expense metrics
            var totalExpenses = data.Sum(e => e.Amount);
            var expensesByCategory = data.GroupBy(e => e.Category)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Amount));
            var averageExpense = data.Average(e => e.Amount);
            var maxExpense = data.Max(e => e.Amount);
            
            totalExpenses.Should().BeGreaterThan(0);
            expensesByCategory.Should().NotBeEmpty();
            averageExpense.Should().BeGreaterThan(0);
            maxExpense.Should().BeGreaterThan(averageExpense);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_NegativeAmount_ThrowsValidationError()
        {
            // Arrange
            var createRequest = new
            {
                name = "Invalid Expense",
                date = DateTime.Today,
                amount = -100m,
                category = "Other"
            };
            
            _httpTest.RespondWith("Amount must be positive", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Expenses.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ResponseContent.Should().Contain("Amount must be positive");
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ApprovedExpense_ThrowsError()
        {
            // Arrange
            var updateRequest = new { id = "98765", amount = 500m };
            
            _httpTest.RespondWith("Cannot modify approved expense", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Expenses.UpdateAsync(updateRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_FutureDate_ThrowsValidationError()
        {
            // Arrange
            var createRequest = new
            {
                name = "Future Expense",
                date = DateTime.Today.AddDays(7),
                amount = 100m,
                category = "Other"
            };
            
            _httpTest.RespondWith("Cannot create expense for future date", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Expenses.CreateAsync(createRequest));
            
            exception.ResponseContent.Should().Contain("future date");
        }

        #endregion
    }
}
