using System.Net;
using Celoxis.Api.Exceptions;
using FluentAssertions;
using Xunit;
using Xunit.Categories;

namespace Celoxis.Api.Tests.Exceptions
{
    public class ExceptionTests
    {
        [Fact]
        [UnitTest]
        public void CeloxisApiException_Constructor_SetsAllProperties()
        {
            // Arrange
            const string message = "API request failed";
            const HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            const string responseContent = "Invalid request parameters";

            // Act
            var exception = new CeloxisApiException(message, statusCode, responseContent);

            // Assert
            exception.Message.Should().Be(message);
            exception.StatusCode.Should().Be(statusCode);
            exception.ResponseContent.Should().Be(responseContent);
        }

        [Fact]
        [UnitTest]
        public void CeloxisApiException_InheritsFromException()
        {
            // Arrange & Act
            var exception = new CeloxisApiException("Test", HttpStatusCode.BadRequest, "Content");

            // Assert
            exception.Should().BeAssignableTo<System.Exception>();
        }

        [Fact]
        [UnitTest]
        public void CeloxisRateLimitException_Constructor_SetsCorrectMessage()
        {
            // Arrange
            const string responseContent = "Rate limit exceeded: 600 calls per hour";

            // Act
            var exception = new CeloxisRateLimitException(responseContent);

            // Assert
            exception.Message.Should().Be("API rate limit exceeded (600 calls per hour)");
            exception.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
            exception.ResponseContent.Should().Be(responseContent);
        }

        [Fact]
        [UnitTest]
        public void CeloxisRateLimitException_InheritsFromCeloxisApiException()
        {
            // Arrange & Act
            var exception = new CeloxisRateLimitException("Content");

            // Assert
            exception.Should().BeAssignableTo<CeloxisApiException>();
        }

        [Theory]
        [UnitTest]
        [InlineData(HttpStatusCode.BadRequest, "Bad Request")]
        [InlineData(HttpStatusCode.Unauthorized, "Unauthorized")]
        [InlineData(HttpStatusCode.Forbidden, "Forbidden")]
        [InlineData(HttpStatusCode.NotFound, "Not Found")]
        [InlineData(HttpStatusCode.InternalServerError, "Internal Server Error")]
        public void CeloxisApiException_DifferentStatusCodes_StoreCorrectly(HttpStatusCode statusCode, string statusName)
        {
            // Arrange & Act
            var exception = new CeloxisApiException($"API error: {statusName}", statusCode, $"Error: {statusName}");

            // Assert
            exception.StatusCode.Should().Be(statusCode);
            exception.Message.Should().Contain(statusName);
            exception.ResponseContent.Should().Contain(statusName);
        }

        [Fact]
        [UnitTest]
        public void CeloxisApiException_EmptyResponseContent_HandlesGracefully()
        {
            // Arrange & Act
            var exception = new CeloxisApiException("Error", HttpStatusCode.BadRequest, string.Empty);

            // Assert
            exception.ResponseContent.Should().BeEmpty();
            exception.ResponseContent.Should().NotBeNull();
        }

        [Fact]
        [UnitTest]
        public void CeloxisApiException_NullResponseContent_HandlesGracefully()
        {
            // Arrange & Act
            var exception = new CeloxisApiException("Error", HttpStatusCode.BadRequest, null!);

            // Assert
            exception.ResponseContent.Should().BeNull();
        }

        [Fact]
        [UnitTest]
        public void CeloxisApiException_JsonResponseContent_PreservesFormat()
        {
            // Arrange
            const string jsonContent = @"{""error"": ""Invalid field"", ""field"": ""budget"", ""value"": ""-1000""}";

            // Act
            var exception = new CeloxisApiException("Validation failed", HttpStatusCode.BadRequest, jsonContent);

            // Assert
            exception.ResponseContent.Should().Be(jsonContent);
            exception.ResponseContent.Should().Contain("\"error\"");
            exception.ResponseContent.Should().Contain("\"field\"");
            exception.ResponseContent.Should().Contain("\"value\"");
        }
    }
}
