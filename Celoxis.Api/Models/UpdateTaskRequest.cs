using System;

namespace Celoxis.Api.Models;

public class UpdateTaskRequest : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? PlannedEffort { get; set; }
    public string? Duration { get; set; }
    public string? Resources { get; set; }
    public DateTimeOffset? PlannedStart { get; set; }
    public DateTimeOffset? PlannedFinish { get; set; }
    public string? Parent { get; set; }
    public string? Priority { get; set; }
    public string? ScheduleType { get; set; }
    public string? ConstraintType { get; set; }
    public DateTimeOffset? ConstraintDate { get; set; }
    public string? BillingType { get; set; }
    public decimal? Budget { get; set; }
    public decimal? FixedPrice { get; set; }
    public int? ActualPercentComplete { get; set; }
    public string? RemainingEffort { get; set; }
    public DateTimeOffset? ActualStart { get; set; }
    public DateTimeOffset? ActualFinish { get; set; }
    public string? Predecessors { get; set; }
    public string? Successors { get; set; }
    public string? Milestone { get; set; }
    public string? IsManuallyScheduled { get; set; }
    public string? IsTimeAllowed { get; set; }
    public string? ShowInTimeline { get; set; }
    public string? ExternalKey { get; set; }
    public string? Color { get; set; }
}
