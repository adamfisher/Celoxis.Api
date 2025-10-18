using System.Text.Json;
using Celoxis.Api.Models;
using Celoxis.Api.Serialization.Converters;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Integration.Converters
{
    [IntegrationTest]
    public class AssociationConverterTests
    {
        [Fact]
        public void ShouldHandleUrlString()
        {
            var json = "\"https://api.example.com/users/123\"";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new AssociationConverter<CeloxisUser>());

            var result = JsonSerializer.Deserialize<Association<CeloxisUser>>(json, options);
            result.Should().NotBeNull();
            result.Url.Should().Be("https://api.example.com/users/123");
            result.Data.Should().BeNull();
            result.IsExpanded.Should().BeFalse();
        }

        [Fact]
        public void ShouldHandleExpandedObject()
        {
            var json = "{\"data\":{\"id\":\"123\",\"name\":\"Test User\",\"email\":\"test@example.com\"}}";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new AssociationConverter<CeloxisUser>());

            var result = JsonSerializer.Deserialize<Association<CeloxisUser>>(json, options);
            result.Should().NotBeNull();
            result.Url.Should().BeNull();
            result.Data.Should().NotBeNull();
            result.IsExpanded.Should().BeTrue();
            result.Data.Id.Should().Be("123");
        }
    }
}
