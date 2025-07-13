using System;

namespace Celoxis.Api.Models;

/// <summary>
/// Request to create a new task
/// </summary>
public class CreateTaskRequest : CeloxisModel
{
    /// <summary>
    /// CeloxisProject ID or code (required)
    /// </summary>
    public string Project { get; set; } = string.Empty;

    /// <summary>
    /// Task name (required)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Planned effort
    /// </summary>
    public string? PlannedEffort { get; set; }

    /// <summary>
    /// Duration
    /// </summary>
    public string? Duration { get; set; }

    /// <summary>
    /// Resources (e.g., "Joe Cool[50%], Peter Parker")
    /// </summary>
    public string? Resources { get; set; }

    /// <summary>
    /// Planned start date
    /// </summary>
    public DateTimeOffset? PlannedStart { get; set; }

    /// <summary>
    /// Parent task ID
    /// </summary>
    public string? Parent { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Priority
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// Schedule type
    /// </summary>
    public string? ScheduleType { get; set; }

    /// <summary>
    /// Constraint type
    /// </summary>
    public string? ConstraintType { get; set; }

    /// <summary>
    /// Constraint date
    /// </summary>
    public DateTimeOffset? ConstraintDate { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    public string? BillingType { get; set; }

    /// <summary>
    /// Budget
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Fixed price
    /// </summary>
    public decimal? FixedPrice { get; set; }
}