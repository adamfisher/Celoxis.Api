@echo off
REM Build and pack the Celoxis.Api NuGet package

echo Building Celoxis.Api...

REM Clean previous builds
dotnet clean

REM Restore dependencies
dotnet restore

REM Build in Release mode
dotnet build --configuration Release

REM Run tests
echo Running tests...
dotnet test --configuration Release --no-build

REM Pack NuGet package
echo Creating NuGet package...
dotnet pack Celoxis.Api\Celoxis.Api.csproj --configuration Release --no-build --output .\nupkg

echo Build complete! NuGet package created in .\nupkg folder
pause
