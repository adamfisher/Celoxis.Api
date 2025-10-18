using System.Text.Json;
using Celoxis.Api.Serialization.Converters;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Integration.Converters
{
    [IntegrationTest]
    public class CeloxisBooleanConverterTests
    {
        [Theory]
        [InlineData("Yes", true)]
        [InlineData("No", false)]
        [InlineData("yes", true)]
        [InlineData("no", false)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        [InlineData("", null)]
        public void ShouldDeserializeCorrectly(string input, bool? expected)
        {
            var json = $"\"{input}\"";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CeloxisBooleanConverter());

            var result = JsonSerializer.Deserialize<bool?>(json, options);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(true, "Yes")]
        [InlineData(false, "No")]
        [InlineData(null, "")]
        public void ShouldSerializeCorrectly(bool? input, string expected)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new CeloxisBooleanConverter());

            var result = JsonSerializer.Serialize(input, options);
            result.Should().Be($"\"{expected}\"");
        }
    }
}
