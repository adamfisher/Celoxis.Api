using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;

namespace Celoxis.Api.Exceptions;

public class CeloxisErrorDetails
{
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    public string? Details { get; set; }
}

public class CeloxisApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ResponseContent { get; }
    public CeloxisErrorDetails? ErrorDetails { get; }

    public CeloxisApiException(string message, HttpStatusCode statusCode, string responseContent) 
        : base(message)
    {
        StatusCode = statusCode;
        ResponseContent = responseContent;
        ErrorDetails = TryParseErrorDetails(responseContent);
    }

    private CeloxisErrorDetails? TryParseErrorDetails(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return null;

        try
        {
            return JsonSerializer.Deserialize<CeloxisErrorDetails>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch
        {
            return new CeloxisErrorDetails { Message = content };
        }
    }
}

public class CeloxisRateLimitException : CeloxisApiException
{
    public CeloxisRateLimitException(string responseContent) 
        : base(ExtractRateLimitMessage(responseContent), HttpStatusCode.TooManyRequests, responseContent)
    {
    }

    private static string ExtractRateLimitMessage(string responseContent)
    {
        if (string.IsNullOrWhiteSpace(responseContent))
            return "API rate limit exceeded";

        var parts = responseContent.Split([':'], 2);
        if (parts.Length == 2)
        {
            return $"API rate limit exceeded ({parts[1].Trim()})";
        }

        return "API rate limit exceeded. Please wait before making more requests.";
    }
}
