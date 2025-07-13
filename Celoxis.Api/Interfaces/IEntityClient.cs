using System.Collections.Generic;
using System.Threading.Tasks;

namespace Celoxis.Api.Interfaces;

/// <summary>
/// Base interface for entity clients
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IEntityClient<T> where T : class
{
    /// <summary>
    /// Get an entity by ID
    /// </summary>
    Task<T> GetByIdAsync(string id, List<string>? expand = null);

    /// <summary>
    /// Get multiple entities by IDs
    /// </summary>
    Task<List<T>> GetByIdsAsync(List<string> ids, List<string>? expand = null);

    /// <summary>
    /// Query entities with optional filtering, sorting, and pagination
    /// </summary>
    Task<(List<T> Data, int TotalRecords, int? NextPage)> QueryAsync(QueryParameters? parameters = null);

    /// <summary>
    /// Create a new entity
    /// </summary>
    Task<T> CreateAsync(object createData);

    /// <summary>
    /// Create multiple entities
    /// </summary>
    Task<List<T>> CreateBatchAsync(List<object> createData);

    /// <summary>
    /// Update an entity
    /// </summary>
    Task<T> UpdateAsync(object updateData);

    /// <summary>
    /// Update multiple entities
    /// </summary>
    Task<List<T>> UpdateBatchAsync(List<object> updateData);

    /// <summary>
    /// Delete an entity
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// Clone an entity
    /// </summary>
    Task<T> CloneAsync(string id, object? overrideData = null);
}