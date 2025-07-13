using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Categories;

namespace Celoxis.Api.Tests
{
    [IntegrationTest]
    public class RealApiTests
    {
        private readonly CeloxisClient _client;

        public RealApiTests()
        {
            var apiKey = "TOKEN_HERE";
            _client = new CeloxisClient(apiKey);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnData()
        {
            var (users, count, _) = await _client.Users.QueryAsync();
            
            users.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetProjects_ShouldReturnData()
        {
            var (projects, count, _) = await _client.Projects.QueryAsync();
            
            projects.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetTasks_ShouldReturnData()
        {
            var (tasks, count, _) = await _client.Tasks.QueryAsync();

            tasks.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetTaskUpdates_ShouldReturnData()
        {
            var (taskUpdates, count, _) = await _client.TaskUpdates.QueryAsync();

            taskUpdates.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetTimeEntries_ShouldReturnData()
        {
            var (timeEntries, count, _) = await _client.TimeEntries.QueryAsync();

            timeEntries.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetApps_ShouldReturnData()
        {
            var (apps, count, _) = await _client.Apps.QueryAsync();

            apps.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetExpenses_ShouldReturnData()
        {
            var (expenses, count, _) = await _client.Expenses.QueryAsync();

            expenses.Should().NotBeNull();
            count.Should().BeGreaterThan(0);
        }
    }
}
