using System.Text.Json.Serialization;

namespace Celoxis.Api.Models
{
    /// <summary>
    /// Response wrapper for API calls that return multiple items
    /// </summary>
    /// <typeparam name="T">Type of data returned</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Response data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;

        /// <summary>
        /// Total number of records matching the query
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }

        /// <summary>
        /// Next page number if more data is available
        /// </summary>
        [JsonPropertyName("nextPage")]
        public int? NextPage { get; set; }
    }
}
