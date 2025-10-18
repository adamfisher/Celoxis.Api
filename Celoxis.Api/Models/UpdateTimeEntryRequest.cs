using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class UpdateTimeEntryRequest : CeloxisModel, IValidatable
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("app")]
    public string? App { get; set; }

    [JsonPropertyName("task")]
    public string? Task { get; set; }

    [JsonPropertyName("user")]
    public string? User { get; set; }

    [JsonPropertyName("billRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BillRate { get; set; }

    [JsonPropertyName("isBillable")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsBillable { get; set; }

    [JsonPropertyName("comments")]
    public string? Comments { get; set; }

    [JsonPropertyName("costRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? CostRate { get; set; }

    [JsonPropertyName("isCostable")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsCostable { get; set; }

    [JsonPropertyName("date")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("externalKey")]
    public string? ExternalKey { get; set; }

    [JsonPropertyName("hours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Hours { get; set; }

    [JsonPropertyName("invoicedOn")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? InvoicedOn { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("timeCode")]
    public string? TimeCode { get; set; }

    public ValidationResult Validate()
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrWhiteSpace(Id))
            errors.Add(new ValidationError("Id", "Time entry ID is required for updates"));

        if (Hours.HasValue && Hours <= 0)
            errors.Add(new ValidationError("Hours", "Hours must be greater than 0"));

        return new ValidationResult(errors);
    }
}
