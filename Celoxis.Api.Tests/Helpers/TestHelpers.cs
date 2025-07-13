using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Celoxis.Api.Tests.Helpers
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendAsync;

        public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendAsync)
        {
            _sendAsync = sendAsync;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _sendAsync(request, cancellationToken);
        }
    }

    public static class HttpClientTestHelper
    {
        public static HttpClient CreateMockHttpClient(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
        {
            var handler = new MockHttpMessageHandler((request, cancellation) =>
            {
                var response = responseFactory(request);
                return Task.FromResult(response);
            });

            return new HttpClient(handler);
        }

        public static HttpResponseMessage CreateJsonResponse<T>(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        public static HttpResponseMessage CreateErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message, Encoding.UTF8, "text/plain")
            };
        }
    }

    public abstract class TestBase
    {
        protected JsonSerializerOptions JsonOptions { get; }

        protected TestBase()
        {
            JsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        protected string GetTestAccessToken() => "test-access-token";

        protected CeloxisClient CreateClientWithMockHttp(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
        {
            var httpClient = HttpClientTestHelper.CreateMockHttpClient(responseFactory);
            var client = new CeloxisClient(GetTestAccessToken());
            
            // Use reflection to replace the HttpClient
            var httpClientField = typeof(CeloxisClient).GetField("_httpClient", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            httpClientField?.SetValue(client, httpClient);
            
            return client;
        }
    }
}
