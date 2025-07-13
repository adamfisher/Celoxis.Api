using System;

namespace Celoxis.Api.Models;

/// <summary>
/// Request to create a new project
/// </summary>
public class CreateProjectRequest : CeloxisModel
{
    /// <summary>
    /// CeloxisProject name (required)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// CeloxisProject manager (required)
    /// </summary>
    public string Manager { get; set; } = string.Empty;

    /// <summary>
    /// Planned start date (required)
    /// </summary>
    public DateTimeOffset? PlannedStart { get; set; }

    /// <summary>
    /// State (required)
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Type (required)
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Workspace (required)
    /// </summary>
    public string Workspace { get; set; } = string.Empty;

    /// <summary>
    /// Deadline
    /// </summary>
    public DateTimeOffset? Deadline { get; set; }

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Budget
    /// </summary>
    public decimal? Budget { get; set; }

    /// <summary>
    /// Billing type
    /// </summary>
    public string? BillingType { get; set; }

    /// <summary>
    /// Fixed price
    /// </summary>
    public decimal? FixedPrice { get; set; }

    /// <summary>
    /// Priority
    /// </summary>
    public string? Priority { get; set; }

    /// <summary>
    /// Risk
    /// </summary>
    public string? Risk { get; set; }

    /// <summary>
    /// Alignment (0-100)
    /// </summary>
    public int? Alignment { get; set; }

    /// <summary>
    /// Benefit (0-100)
    /// </summary>
    public int? Benefit { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Clients
    /// </summary>
    public string? Clients { get; set; }
}