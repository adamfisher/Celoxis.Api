using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Celoxis.Api.Serialization.Converters;

namespace Celoxis.Api.Models;

public class CreateTaskUpdateRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("task")]
    public string Task { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("comments")]
    public string? Comments { get; set; }

    [JsonPropertyName("percentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? PercentComplete { get; set; }

    [JsonPropertyName("actualStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualStart { get; set; }

    [JsonPropertyName("actualFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinish { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Task))
            errors.Add(new ValidationError("Task", "Task ID is required"));

        if (!Date.HasValue)
            errors.Add(new ValidationError("Date", "Date is required"));

        if (PercentComplete.HasValue && (PercentComplete < 0 || PercentComplete > 100))
            errors.Add(new ValidationError("PercentComplete", "Percent complete must be between 0 and 100"));

        return new ValidationResult(errors);
    }
}
