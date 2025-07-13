# Celoxis API Client - Developer Guide

## Building the Project

### Prerequisites
- .NET 6.0 SDK or later
- Visual Studio 2022, VS Code, or JetBrains Rider

### Build Steps

1. Clone or download the project
2. Open a terminal in the project directory
3. Run the build script:
   - On Windows: `build.cmd`
   - On Linux/Mac: `./build.sh`

Or manually:
```bash
dotnet restore
dotnet build
dotnet test
dotnet pack Celoxis.Api/Celoxis.Api.csproj -c Release -o ./nupkg
```

## Testing

The project uses a comprehensive test suite with:
- **xUnit** - Test framework
- **FluentAssertions** - Readable assertions
- **Flurl.Http.Testing** - HTTP mocking
- **Bogus** - Test data generation
- **Moq** - General mocking (if needed)

### Running Tests

Run all tests:
```bash
dotnet test
```

Run with coverage:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

Run specific test category:
```bash
dotnet test --filter Category=UnitTest
dotnet test --filter Category=IntegrationTest
```

### Test Structure

```
Celoxis.Api.Tests/
├── Clients/              # Tests for each entity client
│   ├── ProjectsClientTests.cs
│   ├── TasksClientTests.cs
│   └── ...
├── Exceptions/           # Exception tests
├── Helpers/              # Test utilities and data generators
├── Integration/          # End-to-end scenarios
├── CeloxisClientTests.cs # Main client tests
└── QueryBuilderTests.cs  # Query builder tests
```

### Writing Tests

Example test using Flurl.Http.Testing:

```csharp
[Fact]
[UnitTest]
public async Task GetByIdAsync_ValidId_ReturnsProject()
{
    // Arrange
    using var httpTest = new HttpTest();
    var project = TestDataGenerator.GetProjectFaker().Generate();
    httpTest.RespondWithJson(new { data = project });
    
    var client = new CeloxisClient("test-token");
    
    // Act
    var result = await client.Projects.GetByIdAsync(project.Id);
    
    // Assert
    result.Should().NotBeNull();
    result.Id.Should().Be(project.Id);
    
    httpTest.ShouldHaveCalled("*/api/v2/projects/*")
        .WithHeader("Authorization", "bearer test-token")
        .Times(1);
}
```

## Publishing to NuGet

1. Create an account on [nuget.org](https://www.nuget.org/)
2. Generate an API key
3. Update the package metadata in `Celoxis.Api.csproj`
4. Publish:
   ```bash
   dotnet nuget push ./nupkg/Celoxis.Api.2.0.0.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
   ```

## Using in Your Project

### From NuGet
```bash
dotnet add package Celoxis.Api
```

### From Local Build
```bash
dotnet add package Celoxis.Api --source /path/to/nupkg
```

### From Project Reference
```xml
<ItemGroup>
  <ProjectReference Include="../Celoxis/Celoxis.Api/Celoxis.Api.csproj" />
</ItemGroup>
```

## Architecture

### Key Design Decisions

1. **Flurl.Http** - Chosen for its clean API and excellent testing support
2. **Interface-based** - All clients implement interfaces for easy mocking
3. **Generic EntityClient** - Base class for common CRUD operations
4. **Fluent QueryBuilder** - Intuitive API for building complex queries
5. **Separate Request/Response Models** - Clear separation of concerns

### Project Structure

```
Celoxis.Api/
├── Clients/              # Entity-specific clients
├── Exceptions/           # Custom exceptions
├── Interfaces/           # Client interfaces
├── Models/               # Entity and request models
├── CeloxisClient.cs      # Main client
├── QueryBuilder.cs       # Query construction
└── Constants.cs          # API constants
```

## API Coverage

Currently supported endpoints:
- ✅ Projects (full CRUD + clone)
- ✅ Tasks (full CRUD + clone + associations)
- ✅ Time Entries (full CRUD)
- ✅ Users (read-only)
- ✅ Apps (full CRUD)
- ✅ Expenses (full CRUD)
- ✅ Task Updates (full CRUD)

## Known Limitations

1. Maximum 10 IDs per batch query
2. Maximum 250 records per page
3. 600 API calls per hour limit
4. Users and clients cannot be deleted via API
5. Clone operation is in beta (only for projects, tasks, users)

## Troubleshooting

### Common Issues

1. **401 Unauthorized**: Check your access token
2. **429 Too Many Requests**: You've hit the rate limit (600/hour)
3. **Empty results**: Check your filter syntax
4. **Null reference exceptions**: Enable nullable reference types in your project

### Debug Tips

Enable Flurl logging:
```csharp
FlurlHttp.Configure(settings =>
{
    settings.BeforeCall = call => Console.WriteLine($"Calling: {call.Request.Url}");
    settings.AfterCall = call => Console.WriteLine($"Response: {call.Response.StatusCode}");
});
```

## Contributing Guidelines

1. Fork the repository
2. Create a feature branch
3. Write tests for new functionality
4. Ensure all tests pass
5. Update documentation
6. Submit a pull request

### Code Style

- Use C# naming conventions
- Keep methods focused and small
- Document public APIs with XML comments
- Use nullable reference types
- Prefer immutability where possible

## Version History

- 2.0.0 - Rebuilt with Flurl.Http for better testing support
- 1.0.0 - Initial release with HttpClient

## Resources

- [Celoxis API Documentation](https://www.celoxis.com/api-documentation)
- [Flurl Documentation](https://flurl.dev/)
- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
