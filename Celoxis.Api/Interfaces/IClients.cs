using Celoxis.Api.Models;

namespace Celoxis.Api.Interfaces;

/// <summary>
/// Interface for project operations
/// </summary>
public interface IProjectsClient : IEntityClient<CeloxisProject> { }

/// <summary>
/// Interface for time entry operations
/// </summary>
public interface ITimeEntriesClient : IEntityClient<CeloxisTimeEntry> { }

/// <summary>
/// Interface for app operations
/// </summary>
public interface IAppsClient : IEntityClient<CeloxisApp> { }

/// <summary>
/// Interface for user operations
/// </summary>
public interface IUsersClient : IEntityClient<CeloxisUser> { }

/// <summary>
/// Interface for expense operations
/// </summary>
public interface IExpensesClient : IEntityClient<CeloxisExpense> { }

/// <summary>
/// Interface for task update operations
/// </summary>
public interface ITaskUpdatesClient : IEntityClient<TaskUpdate> { }