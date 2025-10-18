using System.Text.Json;
using Celoxis.Api.Serialization.Converters;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Integration.Converters
{
    [IntegrationTest]
    public class StringToDecimalConverterTests
    {
        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("0", 0.0)]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void ShouldDeserializeCorrectly(string input, double? expectedDouble)
        {
            var expected = expectedDouble.HasValue ? (decimal?)expectedDouble.Value : null;
            var json = input == null ? "null" : $"\"{input}\"";
            var options = new JsonSerializerOptions();
            options.Converters.Add(new StringToDecimalConverter());

            var result = JsonSerializer.Deserialize<decimal?>(json, options);
            result.Should().Be(expected);
        }
    }
}
