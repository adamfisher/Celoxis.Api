using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients
{
    /// <summary>
    /// Base class for entity-specific clients
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class EntityClient<T> : IEntityClient<T> where T : class
    {
        protected readonly CeloxisClient _client;
        private IEntityClient<T> _entityClientImplementation;

        /// <summary>
        /// API endpoint path for this entity
        /// </summary>
        protected abstract string EntityPath { get; }

        /// <summary>
        /// Initializes a new instance of the EntityClient class
        /// </summary>
        /// <param name="client">Celoxis client instance</param>
        protected EntityClient(CeloxisClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc />
        public virtual async Task<T> GetByIdAsync(string id, List<string>? expand = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            var parameters = expand?.Any() == true ? new QueryParameters { Expand = expand } : null;
            var response = await _client.GetAsync<SingleResponse<T>>($"{EntityPath}/{id}", parameters);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> GetByIdsAsync(List<string> ids, List<string>? expand = null)
        {
            if (ids == null || !ids.Any())
                throw new ArgumentException("IDs cannot be null or empty", nameof(ids));

            if (ids.Count > 10)
                throw new ArgumentException("Cannot query more than 10 IDs at a time", nameof(ids));

            var parameters = expand?.Any() == true ? new QueryParameters { Expand = expand } : null;
            var idString = string.Join(",", ids);
            var response = await _client.GetAsync<ApiResponse<List<T>>>($"{EntityPath}/{idString}", parameters);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task<(List<T> Data, int TotalRecords, int? NextPage)> QueryAsync(QueryParameters? parameters = null)
        {
            var response = await _client.GetAsync<ApiResponse<List<T>>>(EntityPath, parameters);
            return (response.Data, response.TotalRecords, response.NextPage);
        }

        /// <inheritdoc />
        public virtual async Task<T> CreateAsync(object createData)
        {
            if (createData == null)
                throw new ArgumentNullException(nameof(createData));

            var response = await _client.PostAsync<SingleResponse<T>>(EntityPath, createData);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> CreateBatchAsync(List<object> createData)
        {
            if (createData == null || !createData.Any())
                throw new ArgumentException("Create data cannot be null or empty", nameof(createData));

            var response = await _client.PostAsync<ApiResponse<List<T>>>(EntityPath, createData);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task<T> UpdateAsync(object updateData)
        {
            if (updateData == null)
                throw new ArgumentNullException(nameof(updateData));

            var response = await _client.PatchAsync<SingleResponse<T>>(EntityPath, updateData);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task<List<T>> UpdateBatchAsync(List<object> updateData)
        {
            if (updateData == null || !updateData.Any())
                throw new ArgumentException("Update data cannot be null or empty", nameof(updateData));

            var response = await _client.PatchAsync<ApiResponse<List<T>>>(EntityPath, updateData);
            return response.Data;
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            await _client.DeleteAsync($"{EntityPath}/{id}");
        }

        /// <inheritdoc />
        public virtual async Task<T> CloneAsync(string id, object? overrideData = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            var response = await _client.PostAsync<SingleResponse<T>>($"{EntityPath}/{id}/clone", overrideData ?? new { });
            return response.Data;
        }
    }
}
