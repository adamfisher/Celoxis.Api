#!/bin/bash

# Build and pack the Celoxis.Api NuGet package

echo "Building Celoxis.Api..."

# Clean previous builds
dotnet clean

# Restore dependencies
dotnet restore

# Build in Release mode
dotnet build --configuration Release

# Run tests
echo "Running tests..."
dotnet test --configuration Release --no-build

# Pack NuGet package
echo "Creating NuGet package..."
dotnet pack Celoxis.Api/Celoxis.Api.csproj --configuration Release --no-build --output ./nupkg

echo "Build complete! NuGet package created in ./nupkg folder"
