using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class UpdateTaskRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("plannedEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedEffort { get; set; }

    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [JsonPropertyName("resources")]
    public string? Resources { get; set; }

    [JsonPropertyName("plannedStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedStart { get; set; }

    [JsonPropertyName("plannedFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedFinish { get; set; }

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("scheduleType")]
    public string? ScheduleType { get; set; }

    [JsonPropertyName("constraintType")]
    public string? ConstraintType { get; set; }

    [JsonPropertyName("constraintDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ConstraintDate { get; set; }

    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    [JsonPropertyName("budget")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Budget { get; set; }

    [JsonPropertyName("fixedPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? FixedPrice { get; set; }

    [JsonPropertyName("actualPercentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? ActualPercentComplete { get; set; }

    [JsonPropertyName("remainingEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? RemainingEffort { get; set; }

    [JsonPropertyName("actualStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualStart { get; set; }

    [JsonPropertyName("actualFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("predecessors")]
    public string? Predecessors { get; set; }

    [JsonPropertyName("successors")]
    public string? Successors { get; set; }

    [JsonPropertyName("milestone")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Milestone { get; set; }

    [JsonPropertyName("isManuallyScheduled")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsManuallyScheduled { get; set; }

    [JsonPropertyName("isTimeAllowed")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsTimeAllowed { get; set; }

    [JsonPropertyName("showInTimeline")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? ShowInTimeline { get; set; }

    [JsonPropertyName("externalKey")]
    public string? ExternalKey { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Id))
            errors.Add(new ValidationError("Id", "Task ID is required for updates"));

        if (ActualPercentComplete.HasValue && (ActualPercentComplete < 0 || ActualPercentComplete > 100))
            errors.Add(new ValidationError("ActualPercentComplete", "Actual percent complete must be between 0 and 100"));

        return new ValidationResult(errors);
    }
}
