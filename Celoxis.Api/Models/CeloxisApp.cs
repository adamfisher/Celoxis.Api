using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

/// <summary>
/// Represents an app (bug, issue, etc.)
/// </summary>
public class CeloxisApp : CeloxisModel
{
    /// <summary>
    /// App ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// App URL
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// App type (e.g., Bug)
    /// </summary>
    public string App { get; set; } = string.Empty;

    /// <summary>
    /// State
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Age
    /// </summary>
    public string Age { get; set; } = string.Empty;

    /// <summary>
    /// Requestor
    /// </summary>
    public string Requestor { get; set; } = string.Empty;

    /// <summary>
    /// Due date
    /// </summary>
    public DateTimeOffset? DueDate { get; set; }

    /// <summary>
    /// Date created
    /// </summary>
    public DateTimeOffset? Created { get; set; }

    /// <summary>
    /// Timeout date
    /// </summary>
    public DateTimeOffset? Timeout { get; set; }

    /// <summary>
    /// Date completed
    /// </summary>
    public DateTimeOffset? Completed { get; set; }

    /// <summary>
    /// Priority
    /// </summary>
    public string Priority { get; set; } = string.Empty;

    /// <summary>
    /// Last update
    /// </summary>
    public string LastUpdate { get; set; } = string.Empty;

    /// <summary>
    /// Last updated on
    /// </summary>
    public DateTimeOffset? LastUpdatedOn { get; set; }

    /// <summary>
    /// All updates
    /// </summary>
    public string AllUpdates { get; set; } = string.Empty;

    /// <summary>
    /// Is delayed
    /// </summary>
    public string Delayed { get; set; } = string.Empty;

    /// <summary>
    /// Is requestor visible
    /// </summary>
    public string RequestorVisible { get; set; } = "No";

    /// <summary>
    /// Is open
    /// </summary>
    public string Open { get; set; } = string.Empty;

    /// <summary>
    /// State manager
    /// </summary>
    public string StateManager { get; set; } = string.Empty;

    /// <summary>
    /// Actual revenue
    /// </summary>
    public decimal ActualRevenue { get; set; }

    /// <summary>
    /// Actual cost
    /// </summary>
    public decimal ActualCost { get; set; }

    /// <summary>
    /// Actual effort
    /// </summary>
    public string ActualEffort { get; set; } = string.Empty;

    /// <summary>
    /// CeloxisProject URL or data (when expanded)
    /// </summary>
    public object Project { get; set; } = string.Empty;

    /// <summary>
    /// Assignee URL or data (when expanded)
    /// </summary>
    public object? Assignee { get; set; }
}