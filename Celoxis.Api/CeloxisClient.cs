using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Celoxis.Api.Clients;
using Celoxis.Api.Exceptions;
using Celoxis.Api.Interfaces;
using Flurl;
using Flurl.Http;

namespace Celoxis.Api
{
    /// <summary>
    /// Main client for interacting with the Celoxis API
    /// </summary>
    public class CeloxisClient : ICeloxisClient
    {
        private readonly string _baseUrl;

        private readonly IFlurlClient _flurlClient;

        /// <summary>
        /// Client for project operations
        /// </summary>
        public IProjectsClient Projects { get; }

        /// <summary>
        /// Client for task operations
        /// </summary>
        public ITasksClient Tasks { get; }

        /// <summary>
        /// Client for time entry operations
        /// </summary>
        public ITimeEntriesClient TimeEntries { get; }

        /// <summary>
        /// Client for app operations
        /// </summary>
        public IAppsClient Apps { get; }

        /// <summary>
        /// Client for user operations
        /// </summary>
        public IUsersClient Users { get; }

        /// <summary>
        /// Client for expense operations
        /// </summary>
        public IExpensesClient Expenses { get; }

        /// <summary>
        /// Client for task update operations
        /// </summary>
        public ITaskUpdatesClient TaskUpdates { get; }

        /// <summary>
        /// Initializes a new instance of the CeloxisClient
        /// </summary>
        /// <param name="accessToken">Your Celoxis API access token</param>
        /// <param name="baseUrl">Base URL of the Celoxis instance (defaults to https://app.celoxis.com/psa)</param>
        /// <param name="flurlClient">Optional Flurl client for testing</param>
        public CeloxisClient(string accessToken, string baseUrl = "https://app.celoxis.com/psa", IFlurlClient? flurlClient = null)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Access token cannot be null or empty", nameof(accessToken));

            _baseUrl = baseUrl;
            _flurlClient = flurlClient ?? new FlurlClient();
            
            // Configure Flurl client
            _flurlClient.BeforeCall(call =>
            {
                call.Request.WithHeader("Authorization", $"bearer {accessToken}")
                    .WithHeader("Content-Type", "application/json");
            });

            _flurlClient.OnError(call =>
            {
                // Don't throw on non-success status codes, we'll handle them
                call.ExceptionHandled = true;
            });

            Projects = new ProjectsClient(this);
            Tasks = new TasksClient(this);
            TimeEntries = new TimeEntriesClient(this);
            Apps = new AppsClient(this);
            Users = new UsersClient(this);
            Expenses = new ExpensesClient(this);
            TaskUpdates = new TaskUpdatesClient(this);
        }

        internal async Task<T> GetAsync<T>(string endpoint, QueryParameters? parameters = null)
        {
            var url = BuildUrl(endpoint, parameters);
            
            var response = await _flurlClient
                .Request(url)
                .GetAsync();

            await EnsureSuccessStatusCode(response);
            
            var responseContent = await response.GetStringAsync();
            if (string.IsNullOrWhiteSpace(responseContent))
                throw new CeloxisApiException($"API returned empty response for {url}", (HttpStatusCode)response.StatusCode, responseContent);
            
            try
            {
                var result = await response.GetJsonAsync<T>();
                if (result == null)
                    throw new CeloxisApiException($"Failed to deserialize response for {url}. Content: {responseContent}", (HttpStatusCode)response.StatusCode, responseContent);
                    
                return result;
            }
            catch (Exception ex) when (!(ex is CeloxisApiException))
            {
                throw new CeloxisApiException($"JSON deserialization failed for {url}. Content: {responseContent}. Error: {ex.Message}", (HttpStatusCode)response.StatusCode, responseContent);
            }
        }

        internal async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var url = $"{_baseUrl}{endpoint}";
            
            var response = await _flurlClient
                .Request(url)
                .PostJsonAsync(data);

            await EnsureSuccessStatusCode(response);
            
            var result = await response.GetJsonAsync<T>();
            if (result == null)
                throw new CeloxisApiException("API returned null response", (HttpStatusCode)response.StatusCode, await response.GetStringAsync());
                
            return result;
        }

        internal async Task<T> PatchAsync<T>(string endpoint, object data)
        {
            var url = $"{_baseUrl}{endpoint}";
            
            var response = await _flurlClient
                .Request(url)
                .PatchJsonAsync(data);

            await EnsureSuccessStatusCode(response);
            
            var result = await response.GetJsonAsync<T>();
            if (result == null)
                throw new CeloxisApiException("API returned null response", (HttpStatusCode)response.StatusCode, await response.GetStringAsync());
                
            return result;
        }

        internal async Task DeleteAsync(string endpoint)
        {
            var url = $"{_baseUrl}{endpoint}";
            
            var response = await _flurlClient
                .Request(url)
                .DeleteAsync();

            await EnsureSuccessStatusCode(response);
        }

        private string BuildUrl(string endpoint, QueryParameters? parameters)
        {
            var url = $"{_baseUrl}{endpoint}";
            if (parameters == null) return url;

            var flurlUrl = url.SetQueryParams(new Dictionary<string, object?>());

            if (parameters.Filter != null)
            {
                flurlUrl.SetQueryParam("filter", System.Text.Json.JsonSerializer.Serialize(parameters.Filter));
            }

            if (!string.IsNullOrEmpty(parameters.Sort))
            {
                flurlUrl.SetQueryParam("sort", parameters.Sort);
            }

            if (parameters.Page.HasValue)
            {
                flurlUrl.SetQueryParam("page", parameters.Page.Value);
            }

            if (parameters.Expand?.Any() == true)
            {
                flurlUrl.SetQueryParam("expand", string.Join(",", parameters.Expand));
            }

            return flurlUrl.ToString();
        }

        private async Task EnsureSuccessStatusCode(IFlurlResponse response)
        {
            if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                return;

            var content = await response.GetStringAsync();
            
            if (response.StatusCode == 429)
            {
                throw new CeloxisRateLimitException(content);
            }

            throw new CeloxisApiException($"API request failed: {response.StatusCode}", (HttpStatusCode) response.StatusCode, content);
        }
    }
}
