using System;
using System.Net;

namespace Celoxis.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when a Celoxis API call fails
    /// </summary>
    public class CeloxisApiException : Exception
    {
        /// <summary>
        /// HTTP status code of the failed response
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Raw response content from the API
        /// </summary>
        public string ResponseContent { get; }

        /// <summary>
        /// Initializes a new instance of the CeloxisApiException class
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="statusCode">HTTP status code</param>
        /// <param name="responseContent">Response content from the API</param>
        public CeloxisApiException(string message, HttpStatusCode statusCode, string responseContent) 
            : base(message)
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }
    }

    /// <summary>
    /// Exception thrown when the API rate limit is exceeded
    /// </summary>
    public class CeloxisRateLimitException : CeloxisApiException
    {
        /// <summary>
        /// Initializes a new instance of the CeloxisRateLimitException class
        /// </summary>
        public CeloxisRateLimitException(string responseContent) 
            : base("API rate limit exceeded (600 calls per hour)", HttpStatusCode.TooManyRequests, responseContent)
        {
        }
    }
}
