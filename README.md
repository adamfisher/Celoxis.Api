# Celoxis.Api

A .NET client library for the [Celoxis API v2](https://www.celoxis.com/), providing easy access to projects, tasks, time entries, and more. Built with `Flurl.Http` for a clean, testable HTTP client experience.

## Features

- Full support for Celoxis API v2 endpoints
- Strongly-typed models for all entities
- Fluent query builder for complex filtering
- Async/await support throughout
- Built on Flurl.Http for easy testing and mocking
- Built for .NET 6+

## Installation

Install via NuGet:

```bash
dotnet add package Celoxis.Api
```

Or via Package Manager Console:

```powershell
Install-Package Celoxis.Api
```

## Quick Start

```csharp
using Celoxis.Api;
using Celoxis.Api.Models;

// Initialize the client
var client = new CeloxisClient("YourAccessToken");

// Get a project by ID
var project = await client.Projects.GetByIdAsync("1234");

// Query projects with filters
var query = new QueryBuilder()
    .Where("state", States.Project.Active)
    .Where("budget", ">50000")
    .OrderBy("plannedStart", descending: true)
    .Expand("manager", "clients");

var (projects, totalRecords, nextPage) = await client.Projects.QueryAsync(query.Build());

// Create a new project
var newProject = await client.Projects.CreateAsync(new CreateProjectRequest
{
    Name = "New Website Development",
    Manager = "Tony Stark",
    PlannedStart = DateTimeOffset?.Now.Date,
    State = States.Project.Active,
    Type = "Infrastructure",
    Workspace = "Vimi",
    Budget = 50000
});
```

## Dependency Injection

Easily integrate with ASP.NET Core or other DI containers:

```csharp
using Celoxis.Api;

// In Program.cs or Startup.cs
services.AddCeloxisApi(options =>
{
    options.AccessToken = "your-access-token";
    options.BaseUrl = "https://your-instance.celoxis.com/psa"; // optional
});

// Or with direct options
services.AddCeloxisApi(new CeloxisOptions
{
    AccessToken = "your-access-token"
});

// Inject into controllers or services
public class ProjectController : ControllerBase
{
    private readonly ICeloxisClient _celoxisClient;

    public ProjectController(ICeloxisClient celoxisClient)
    {
        _celoxisClient = celoxisClient;
    }

    public async Task<IActionResult> GetProject(string id)
    {
        var project = await _celoxisClient.Projects.GetByIdAsync(id);
        return Ok(project);
    }
}
```

## Authentication

To use the API, you need a permanent access token from Celoxis. This token should be kept secret and treated like a password.

```csharp
var client = new CeloxisClient("your-permanent-access-token");
```

## API Rate Limits

The Celoxis API has a limit of 600 calls per hour. The library will throw a `CeloxisRateLimitException` if this limit is exceeded. You should request additional capacity from Celoxis if needed.

## Entities Supported

- **CeloxisProject** - Project management
- **CeloxisTask** - Task management with predecessors and assignments
- **CeloxisTimeEntry** - Time tracking
- **CeloxisUser** - User management (read-only, no delete)
- **CeloxisApp** - Custom apps (bugs, issues, etc.)
- **Expense** - Expense tracking
- **UpdateTaskRequest** - Task comments and updates

## Query Building

The library includes a powerful query builder for complex filtering:

```csharp
var query = new QueryBuilder()
    // Simple equality
    .Where("name", "Project X")
    
    // Starts with
    .WhereStartsWith("code", "PRJ")
    
    // Multiple values (IN)
    .WhereIn("state", "Active", "Draft")
    
    // Date ranges
    .Where("plannedStart", DateFilters.ThisMonth)
    .Where("deadline", DateFilters.Range(startDate, endDate))
    
    // Nested properties
    .Where("project.manager.name", "John Doe")
    
    // Sorting
    .OrderBy("budget", descending: true)
    .OrderBy("plannedStart")
    
    // Expand associations
    .Expand("manager", "clients", "tasks")
    
    // Pagination
    .Page(2);
```

## Date Filters

The library includes convenient date filter constants:

```csharp
DateFilters.Today
DateFilters.ThisWeek
DateFilters.LastMonth
DateFilters.NextQuarter
DateFilters.ThisYear
DateFilters.Range(from, to)
DateFilters.LastNDays(7)
DateFilters.NextNDays(30)
```

## Custom Fields

Access custom fields using their formula keys:

```csharp
// In queries
query.Where("custom_country", "USA");

// In models (with JsonPropertyName attributes)
project.SOWHours; // Maps to custom_cf663672
```

## Error Handling

```csharp
try
{
    var project = await client.Projects.GetByIdAsync("999999");
}
catch (CeloxisRateLimitException ex)
{
    // Handle rate limit (429)
    Console.WriteLine("Rate limit exceeded. Try again later.");
}
catch (CeloxisApiException ex)
{
    // Handle other API errors
    Console.WriteLine($"API Error: {ex.StatusCode}");
    Console.WriteLine($"Response: {ex.ResponseContent}");
}
```

## Testing

The library is built on Flurl.Http, making it easy to test your code:

```csharp
using Flurl.Http.Testing;

[Fact]
public async Task GetProject_ReturnsProject()
{
    using var httpTest = new HttpTest();
    
    // Arrange
    httpTest.RespondWithJson(new { data = new { id = "123", name = "Test Project" } });
    
    var client = new CeloxisClient("test-token");
    
    // Act
    var project = await client.Projects.GetByIdAsync("123");
    
    // Assert
    Assert.Equal("Test Project", project.Name);
    httpTest.ShouldHaveCalled("*/api/v2/projects/123");
}
```

## Advanced Examples

### Batch Operations

```csharp
// Create multiple tasks
var tasks = await client.Tasks.CreateBatchAsync(new List<object>
{
    new CreateTaskRequest { Name = "Task 1", Project = "X123" },
    new CreateTaskRequest { Name = "Task 2", Project = "X123" }
});

// Update multiple projects
var updates = await client.Projects.UpdateBatchAsync(new List<object>
{
    new { id = "1234", priority = Priorities.High },
    new { id = "5678", priority = Priorities.VeryHigh }
});
```

### Working with Associations

```csharp
// Get task with expanded project data
var task = await client.Tasks.GetByIdAsync("123", new List<string> { "project" });

// Get task predecessors
var predecessors = await client.Tasks.GetPredecessorsAsync("123");
```

### Pagination

```csharp
var allProjects = new List<CeloxisProject>();
var page = 1;

do
{
    var query = new QueryBuilder()
        .Where("state", States.Project.Active)
        .Page(page);

    var (projects, total, nextPage) = await client.Projects.QueryAsync(query.Build());
    allProjects.AddRange(projects);
    
    page = nextPage ?? 0;
} while (page > 0);
```

### Cloning Entities

```csharp
// Clone a project (Beta feature)
var clonedProject = await client.Projects.CloneAsync("123", new
{
    name = "Cloned Project",
    plannedStart = DateTimeOffset?.Now,
    code = "CLONE-001"
});
```

### Custom Flurl Client

You can provide your own configured Flurl client for advanced scenarios:

```csharp
var flurlClient = new FlurlClient()
    .Configure(settings =>
    {
        settings.Timeout = TimeSpan.FromSeconds(30);
        settings.BeforeCall = call => Console.WriteLine($"Calling: {call.Request.Url}");
    });

var client = new CeloxisClient("token", "https://app.celoxis.com/psa", flurlClient);
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

For API-specific questions, consult the [Celoxis API documentation](https://www.celoxis.com/api-documentation).

For issues with this library, please open an issue on GitHub.
