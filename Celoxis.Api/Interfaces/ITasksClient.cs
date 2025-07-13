using System.Collections.Generic;
using System.Threading.Tasks;
using Celoxis.Api.Models;

namespace Celoxis.Api.Interfaces;

/// <summary>
/// Interface for task operations
/// </summary>
public interface ITasksClient : IEntityClient<CeloxisTask>
{
    /// <summary>
    /// Get predecessors of a task
    /// </summary>
    Task<List<CeloxisTask>> GetPredecessorsAsync(int taskId);

    /// <summary>
    /// Get successors of a task
    /// </summary>
    Task<List<CeloxisTask>> GetSuccessorsAsync(int taskId);

    /// <summary>
    /// Get assignments for a task
    /// </summary>
    Task<List<object>> GetAssignmentsAsync(int taskId);
}