namespace Celoxis.Api.Interfaces;

/// <summary>
/// Main interface for the Celoxis API client
/// </summary>
public interface ICeloxisClient
{
    /// <summary>
    /// Client for project operations
    /// </summary>
    IProjectsClient Projects { get; }

    /// <summary>
    /// Client for task operations
    /// </summary>
    ITasksClient Tasks { get; }

    /// <summary>
    /// Client for time entry operations
    /// </summary>
    ITimeEntriesClient TimeEntries { get; }

    /// <summary>
    /// Client for app operations
    /// </summary>
    IAppsClient Apps { get; }

    /// <summary>
    /// Client for user operations
    /// </summary>
    IUsersClient Users { get; }

    /// <summary>
    /// Client for expense operations
    /// </summary>
    IExpensesClient Expenses { get; }

    /// <summary>
    /// Client for task update operations
    /// </summary>
    ITaskUpdatesClient TaskUpdates { get; }
}