using System.Collections.Generic;
using System.Linq;

namespace Celoxis.Api
{
    /// <summary>
    /// Parameters for querying entities
    /// </summary>
    public class QueryParameters
    {
        /// <summary>
        /// Filter criteria as key-value pairs
        /// </summary>
        public Dictionary<string, object>? Filter { get; set; }

        /// <summary>
        /// Sort criteria (comma-separated field names, optionally suffixed with /desc)
        /// </summary>
        public string? Sort { get; set; }

        /// <summary>
        /// Page number for pagination
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Associations to expand in the response
        /// </summary>
        public List<string>? Expand { get; set; }
    }

    /// <summary>
    /// Fluent builder for constructing query parameters
    /// </summary>
    public class QueryBuilder
    {
        private readonly Dictionary<string, object> _filters = new Dictionary<string, object>();
        private readonly List<string> _sortFields = new List<string>();
        private readonly List<string> _expandFields = new List<string>();
        private int? _page;

        /// <summary>
        /// Add a filter condition
        /// </summary>
        /// <param name="field">Field name to filter on</param>
        /// <param name="value">Value to filter by</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder Where(string field, object value)
        {
            _filters[field] = value;
            return this;
        }

        /// <summary>
        /// Add a starts-with filter condition
        /// </summary>
        /// <param name="field">Field name to filter on</param>
        /// <param name="value">Value to match the start of</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder WhereStartsWith(string field, string value)
        {
            _filters[field] = $"^{value}";
            return this;
        }

        /// <summary>
        /// Add an IN filter condition
        /// </summary>
        /// <param name="field">Field name to filter on</param>
        /// <param name="values">Values to match</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder WhereIn(string field, params string[] values)
        {
            _filters[field] = values;
            return this;
        }

        /// <summary>
        /// Add a sort field
        /// </summary>
        /// <param name="field">Field name to sort by</param>
        /// <param name="descending">Whether to sort in descending order</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder OrderBy(string field, bool descending = false)
        {
            _sortFields.Add(descending ? $"{field}/desc" : field);
            return this;
        }

        /// <summary>
        /// Add associations to expand
        /// </summary>
        /// <param name="associations">Association names to expand</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder Expand(params string[] associations)
        {
            _expandFields.AddRange(associations);
            return this;
        }

        /// <summary>
        /// Set the page number
        /// </summary>
        /// <param name="page">Page number</param>
        /// <returns>The query builder for chaining</returns>
        public QueryBuilder Page(int page)
        {
            _page = page;
            return this;
        }

        /// <summary>
        /// Build the query parameters
        /// </summary>
        /// <returns>Query parameters object</returns>
        public QueryParameters Build()
        {
            return new QueryParameters
            {
                Filter = _filters.Any() ? _filters : null,
                Sort = _sortFields.Any() ? string.Join(",", _sortFields) : null,
                Expand = _expandFields.Any() ? _expandFields : null,
                Page = _page
            };
        }
    }
}
