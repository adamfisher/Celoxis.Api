using System.Text.Json;
using Celoxis.Api.Serialization.Converters;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Integration.Converters
{
    [IntegrationTest]
    public class StringToIntConverterTests
    {
        [Theory]
        [InlineData("12345", 12345)]
        [InlineData("0", 0)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void ShouldDeserializeCorrectly(string input, int? expected)
        {
            var json = input == null ? "null" : $"\"{input}\"";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new StringToIntConverter());

            var result = JsonSerializer.Deserialize<int?>(json, options);
            result.Should().Be(expected);
        }
    }
}
