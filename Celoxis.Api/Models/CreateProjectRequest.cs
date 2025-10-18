using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CreateProjectRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("manager")]
    [Required]
    public string Manager { get; set; } = string.Empty;

    [JsonPropertyName("plannedStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    [Required]
    public DateTimeOffset? PlannedStart { get; set; }

    [JsonPropertyName("state")]
    [Required]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [Required]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("workspace")]
    [Required]
    public string Workspace { get; set; } = string.Empty;

    [JsonPropertyName("deadline")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Deadline { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("budget")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Budget { get; set; }

    [JsonPropertyName("billingType")]
    public string? BillingType { get; set; }

    [JsonPropertyName("fixedPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? FixedPrice { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("risk")]
    public string? Risk { get; set; }

    [JsonPropertyName("alignment")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? Alignment { get; set; }

    [JsonPropertyName("benefit")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? Benefit { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("clients")]
    public string? Clients { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Name))
            errors.Add(new ValidationError("Name", "Project name is required"));

        if (string.IsNullOrWhiteSpace(Manager))
            errors.Add(new ValidationError("Manager", "Project manager is required"));

        if (!PlannedStart.HasValue)
            errors.Add(new ValidationError("PlannedStart", "Planned start date is required"));

        if (string.IsNullOrWhiteSpace(State))
            errors.Add(new ValidationError("State", "Project state is required"));

        if (string.IsNullOrWhiteSpace(Type))
            errors.Add(new ValidationError("Type", "Project type is required"));

        if (string.IsNullOrWhiteSpace(Workspace))
            errors.Add(new ValidationError("Workspace", "Workspace is required"));

        if (Alignment.HasValue && (Alignment < 0 || Alignment > 100))
            errors.Add(new ValidationError("Alignment", "Alignment must be between 0 and 100"));

        if (Benefit.HasValue && (Benefit < 0 || Benefit > 100))
            errors.Add(new ValidationError("Benefit", "Benefit must be between 0 and 100"));

        return new ValidationResult(errors);
    }
}