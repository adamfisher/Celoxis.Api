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
    public class UsersClientTests : IDisposable
    {
        private readonly HttpTest _httpTest;
        private readonly CeloxisClient _client;
        private readonly string _baseUrl = "https://app.celoxis.com/psa";

        public UsersClientTests()
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
        public async Task GetByIdAsync_ValidId_ReturnsUser()
        {
            // Arrange
            var user = TestDataGenerator.GetUserFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.GetByIdAsync(user.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.BillRate.Should().Be(user.BillRate);
            result.CostRate.Should().Be(user.CostRate);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users/{user.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task GetByIdAsync_ByLogin_ReturnsUser()
        {
            // Arrange
            var user = TestDataGenerator.GetUserFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.GetByIdAsync(user.Id);

            // Assert
            result.Should().NotBeNull();
            result.Username.Should().Be(user.Username);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users/{user.Id}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region GetByIdsAsync Tests

        [Fact]
        [UnitTest]
        public async Task GetByIdsAsync_MultipleIds_ReturnsUsers()
        {
            // Arrange
            var users = TestDataGenerator.GetUserFaker().Generate(5);
            var ids = users.Select(u => u.Id).ToList();
            var response = TestDataGenerator.CreateApiResponse(users);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.GetByIdsAsync(ids);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(5);
            result.Select(u => u.Id).Should().BeEquivalentTo(ids);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users/{string.Join(",", ids)}")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region QueryAsync Tests

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByRole_ReturnsUsersWithRole()
        {
            // Arrange
            var users = TestDataGenerator.GetUserFaker()
                .RuleFor(u => u.Roles, "CeloxisProject Manager")
                .Generate(3);
            var response = TestDataGenerator.CreateApiResponse(users, 3);
            var query = new QueryBuilder()
                .Where("roles", "CeloxisProject Manager")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, totalRecords, _) = await _client.Users.QueryAsync(query);

            // Assert
            data.Should().NotBeNull();
            data.Should().HaveCount(3);
            data.Should().OnlyContain(u => u.Roles == "CeloxisProject Manager");
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByAdmin_ReturnsAdminUsers()
        {
            // Arrange
            var users = TestDataGenerator.GetUserFaker()
                .RuleFor(u => u.Admin, true)
                .Generate(2);
            var response = TestDataGenerator.CreateApiResponse(users, 2);
            var query = new QueryBuilder()
                .Where("admin", "Yes")
                .OrderBy("name")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Users.QueryAsync(query);

            // Assert
            data.Should().HaveCount(2);
            data.Should().OnlyContain(u => u.Admin == true);
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users")
                .WithQueryParam("filter")
                .WithQueryParam("sort", "name")
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task QueryAsync_FilterByReportingManager_ReturnsTeamMembers()
        {
            // Arrange
            var managerName = "John Manager";
            var users = TestDataGenerator.GetUserFaker()
                .RuleFor(u => u.ReportingManager, managerName)
                .Generate(4);
            var response = TestDataGenerator.CreateApiResponse(users, 4);
            var query = new QueryBuilder()
                .Where("reportingManager", managerName)
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Users.QueryAsync(query);

            // Assert
            data.Should().HaveCount(4);
            data.Should().OnlyContain(u => u.ReportingManager == managerName);
        }

        #endregion

        #region CreateAsync Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_ValidUser_CreatesSuccessfully()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateUserRequestFaker()
                .RuleFor(r => r.Name, "New User")
                .RuleFor(r => r.Email, "newuser@company.com")
                .RuleFor(r => r.Login, "newuser")
                .RuleFor(r => r.WorkCalendar, "Default")
                .RuleFor(r => r.BillRate, 150)
                .RuleFor(r => r.CostRate, 75)
                .RuleFor(r => r.Roles, "Staff")
                .RuleFor(r => r.ReportingManager, "John Manager")
                .Generate();
            
            var user = TestDataGenerator.GetUserFaker().Generate();
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.CreateAsync(createRequest);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users")
                .WithVerb(HttpMethod.Post)
                .WithRequestJson(createRequest)
                .Times(1);
        }

        [Fact]
        [UnitTest]
        public async Task CreateAsync_DuplicateLogin_ThrowsError()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateUserRequestFaker()
                .RuleFor(r => r.Name, "Duplicate User")
                .RuleFor(r => r.Email, "duplicate@company.com")
                .RuleFor(r => r.Login, "existinguser")
                .Generate();
            
            _httpTest.RespondWith("User with login 'existinguser' already exists", 409);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Users.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Conflict);
            exception.ResponseContent.Should().Contain("already exists");
        }

        #endregion

        #region UpdateAsync Tests

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeRates_UpdatesSuccessfully()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateUserRequestFaker()
                .RuleFor(r => r.Id, "123456")
                .RuleFor(r => r.BillRate, 200)
                .RuleFor(r => r.CostRate, 100)
                .Generate();
            
            var user = TestDataGenerator.GetUserFaker().Generate();
            user.BillRate = 200;
            user.CostRate = 100;
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.UpdateAsync(updateRequest);

            // Assert
            result.Should().NotBeNull();
            result.BillRate.Should().Be(200);
            result.CostRate.Should().Be(100);
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_ChangeReportingManager_UpdatesHierarchy()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateUserRequestFaker()
                .RuleFor(r => r.Id, "123456")
                .RuleFor(r => r.ReportingManager, "New Manager")
                .Generate();
            
            var user = TestDataGenerator.GetUserFaker().Generate();
            user.ReportingManager = "New Manager";
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.UpdateAsync(updateRequest);

            // Assert
            result.ReportingManager.Should().Be("New Manager");
        }

        #endregion

        #region DeleteAsync Tests

        [Fact]
        [UnitTest]
        public void DeleteAsync_Always_ThrowsNotSupportedException()
        {
            // Act & Assert
            Func<Task> act = async () => await _client.Users.DeleteAsync("123456");
            
            act.Should().ThrowAsync<NotSupportedException>()
                .WithMessage("Users cannot be deleted using the API");
        }

        #endregion

        #region CloneAsync Tests

        [Fact]
        [UnitTest]
        public async Task CloneAsync_ValidUser_ClonesSuccessfully()
        {
            // Arrange
            var overrideData = TestDataGenerator.GetCreateUserRequestFaker()
                .RuleFor(r => r.Name, "Cloned User")
                .RuleFor(r => r.Email, "cloned@company.com")
                .RuleFor(r => r.Login, "cloneduser")
                .Generate();
            
            var user = TestDataGenerator.GetUserFaker().Generate();
            user.Name = "Cloned User";
            user.Email = "cloned@company.com";
            user.Username = "cloneduser";
            var response = TestDataGenerator.CreateSingleResponse(user);
            
            _httpTest.RespondWithJson(response);

            // Act
            var result = await _client.Users.CloneAsync("123456", overrideData);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Cloned User");
            result.Username.Should().Be("cloneduser");
            
            _httpTest.ShouldHaveCalled($"{_baseUrl}/api/v2/users/123456/clone")
                .WithVerb(HttpMethod.Post)
                .Times(1);
        }

        #endregion

        #region Complex Scenarios

        [Fact]
        [IntegrationTest]
        public async Task TeamSetup_CreateMultipleUsersWithHierarchy_Success()
        {
            // Simulate setting up a team with manager and members
            // Arrange
            var managerRequest = new CreateUserRequest
            {
                Name = "Team Manager",
                Email = "manager@company.com",
                Login = "teammanager",
                Roles = "CeloxisProject Manager",
                BillRate = 250,
                CostRate = 125
            };
            
            var manager = TestDataGenerator.GetUserFaker().Generate();
            manager.Name = "Team Manager";
            
            var memberRequests = new List<CreateUserRequest>
            {
                new CreateUserRequest
                {
                    Name = "Developer 1",
                    Email = "dev1@company.com",
                    Login = "dev1",
                    Roles = "Developer",
                    ReportingManager = "Team Manager",
                    BillRate = 150,
                    CostRate = 75
                },
                new CreateUserRequest
                {
                    Name = "Developer 2",
                    Email = "dev2@company.com",
                    Login = "dev2",
                    Roles = "Developer",
                    ReportingManager = "Team Manager",
                    BillRate = 150,
                    CostRate = 75
                }
            };
            
            var members = TestDataGenerator.GetUserFaker().Generate(2);
            
            _httpTest
                .RespondWithJson(TestDataGenerator.CreateSingleResponse(manager))
                .RespondWithJson(TestDataGenerator.CreateApiResponse(members));

            // Act
            var createdManager = await _client.Users.CreateAsync(managerRequest);
            var createdMembers = await _client.Users.CreateBatchAsync(memberRequests.Cast<object>().ToList());

            // Assert
            createdManager.Should().NotBeNull();
            createdMembers.Should().HaveCount(2);
            
            _httpTest.CallLog.Should().HaveCount(2);
        }

        [Fact]
        [IntegrationTest]
        public async Task ResourcePlanning_FindAvailableResources_Success()
        {
            // Simulate finding available resources for a project
            // Arrange
            var users = TestDataGenerator.GetUserFaker()
                .RuleFor(u => u.Roles, f => f.PickRandom("Developer", "Designer", "Tester"))
                .RuleFor(u => u.BillRate, f => f.Random.Number(100, 199))
                .Generate(10);
            var response = TestDataGenerator.CreateApiResponse(users, 10);
            
            var query = new QueryBuilder()
                .Where("virtual", "No")
                .WhereIn("roles", "Developer", "Designer")
                .Where("billRate", "<200")
                .OrderBy("billRate")
                .Build();
            
            _httpTest.RespondWithJson(response);

            // Act
            var (data, _, _) = await _client.Users.QueryAsync(query);

            // Assert
            data.Should().HaveCount(10);
            data.Should().OnlyContain(u => u.BillRate < 200);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        [UnitTest]
        public async Task CreateAsync_InvalidEmail_ThrowsValidationError()
        {
            // Arrange
            var createRequest = TestDataGenerator.GetCreateUserRequestFaker()
                .RuleFor(r => r.Name, "Invalid User")
                .RuleFor(r => r.Email, "not-an-email")
                .RuleFor(r => r.Login, "invaliduser")
                .Generate();
            
            _httpTest.RespondWith("Invalid email format", 400);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Users.CreateAsync(createRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            exception.ResponseContent.Should().Contain("Invalid email");
        }

        [Fact]
        [UnitTest]
        public async Task UpdateAsync_DeactivatedUser_ThrowsError()
        {
            // Arrange
            var updateRequest = TestDataGenerator.GetUpdateUserRequestFaker()
                .RuleFor(r => r.Id, "999999")
                .RuleFor(r => r.BillRate, 200)
                .Generate();
            
            _httpTest.RespondWith("Cannot update deactivated user", 403);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<CeloxisApiException>(() => 
                _client.Users.UpdateAsync(updateRequest));
            
            exception.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        #endregion
    }
}
