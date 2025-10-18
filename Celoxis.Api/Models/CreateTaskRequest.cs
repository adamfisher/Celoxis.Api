using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CreateTaskRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("project")]
    public string Project { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

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

    [JsonPropertyName("parent")]
    public string? Parent { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

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

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Project))
            errors.Add(new ValidationError("Project", "Project ID or code is required"));

        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationError("Name", "Task name is required"));

        return new ValidationResult(errors);
    }
}
