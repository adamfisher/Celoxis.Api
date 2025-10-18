using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CreateTimeEntryRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("user")]
    public string User { get; set; } = string.Empty;

    [JsonPropertyName("workItem")]
    public string WorkItem { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("hours")]
    public decimal Hours { get; set; }

    [JsonPropertyName("timeCode")]
    public string? TimeCode { get; set; }

    [JsonPropertyName("comments")]
    public string? Comments { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(User))
            errors.Add(new ValidationError("User", "User ID or login is required"));

        if (string.IsNullOrWhiteSpace(WorkItem))
            errors.Add(new ValidationError("WorkItem", "Work item ID is required"));

        if (!Date.HasValue)
            errors.Add(new ValidationError("Date", "Date is required"));

        if (Hours <= 0)
            errors.Add(new ValidationError("Hours", "Hours must be greater than 0"));

        return new ValidationResult(errors);
    }
}
