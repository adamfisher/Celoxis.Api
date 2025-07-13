using System;
using System.Threading.Tasks;
using Celoxis.Api;
using Celoxis.Api.Models;

namespace CeloxisExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Replace with your actual access token
            var accessToken = Environment.GetEnvironmentVariable("CELOXIS_ACCESS_TOKEN") 
                ?? throw new Exception("Please set CELOXIS_ACCESS_TOKEN environment variable");

            var client = new CeloxisClient(accessToken);

            try
            {
                // Example 1: List active projects
                Console.WriteLine("=== Active Projects ===");
                var projectQuery = new QueryBuilder()
                    .Where("state", States.Project.Active)
                    .OrderBy("plannedStart", descending: true)
                    .Page(1);

                var (projects, total, _) = await client.Projects.QueryAsync(projectQuery.Build());
                Console.WriteLine($"Found {total} active projects:");
                
                foreach (var project in projects)
                {
                    Console.WriteLine($"- {project.Name} (Budget: ${project.Budget:N0})");
                }

                // Example 2: Get time entries for current week
                Console.WriteLine("\n=== Time Entries This Week ===");
                var timeQuery = new QueryBuilder()
                    .Where("date", DateFilters.ThisWeek)
                    .Expand("user", "project")
                    .OrderBy("date", descending: true);

                var (timeEntries, _, _) = await client.TimeEntries.QueryAsync(timeQuery.Build());
                
                foreach (var entry in timeEntries)
                {
                    Console.WriteLine($"- {entry.Date:yyyy-MM-dd}: {entry.Hours}h on {entry.Comments}");
                }

                // Example 3: Create a new task
                Console.WriteLine("\n=== Creating New Task ===");
                if (projects.Any())
                {
                    var newTask = await client.Tasks.CreateAsync(new CreateTaskRequest
                    {
                        Project = projects.First().Code ?? projects.First().Id,
                        Name = $"Sample Task - {DateTime.Now:yyyy-MM-dd HH:mm}",
                        PlannedEffort = "8",
                        Duration = "1d",
                        PlannedStart = DateTime.Now.Date.AddDays(1),
                        Description = "This is a sample task created via the API"
                    });

                    Console.WriteLine($"Created task: {newTask.Name} (ID: {newTask.Id})");
                }

                // Example 4: Complex query with custom fields
                Console.WriteLine("\n=== Projects with High Budget ===");
                var highBudgetQuery = new QueryBuilder()
                    .Where("budget", ">100000")
                    .Where("scheduleHealth", "On Track")
                    .OrderBy("budget", descending: true)
                    .Expand("manager");

                var (highBudgetProjects, _, _) = await client.Projects.QueryAsync(highBudgetQuery.Build());
                
                foreach (var project in highBudgetProjects)
                {
                    Console.WriteLine($"- {project.Name}: ${project.Budget:N0}");
                }
            }
            catch (CeloxisApiException ex)
            {
                Console.WriteLine($"API Error: {ex.StatusCode}");
                Console.WriteLine($"Details: {ex.ResponseContent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
