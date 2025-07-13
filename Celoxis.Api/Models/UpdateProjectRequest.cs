using System;

namespace Celoxis.Api.Models;

/// <summary>
/// Request to update a project
/// </summary>
public class UpdateProjectRequest : CeloxisModel
{
    /// <summary>
    /// CeloxisProject ID (required)
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Manager
    /// </summary>
    public string? Manager { get; set; }

    /// <summary>
    /// Planned start
    /// </summary>
    public DateTimeOffset? PlannedStart { get; set; }

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

    /// <summary>
    /// State
    /// </summary>
    public string? State { get; set; }

    /// <summary>
    /// Type
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Workspace
    /// </summary>
    public string? Workspace { get; set; }
}