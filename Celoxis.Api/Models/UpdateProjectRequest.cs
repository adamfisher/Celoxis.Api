using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class UpdateProjectRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("manager")]
    public string? Manager { get; set; }

    [JsonPropertyName("plannedStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedStart { get; set; }

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

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("workspace")]
    public string? Workspace { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Id))
            errors.Add(new ValidationError("Id", "Project ID is required for updates"));

        if (Alignment.HasValue && (Alignment < 0 || Alignment > 100))
            errors.Add(new ValidationError("Alignment", "Alignment must be between 0 and 100"));

        if (Benefit.HasValue && (Benefit < 0 || Benefit > 100))
            errors.Add(new ValidationError("Benefit", "Benefit must be between 0 and 100"));

        return new ValidationResult(errors);
    }
}