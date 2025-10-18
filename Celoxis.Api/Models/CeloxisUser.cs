using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Celoxis.Api.Serialization.Converters;

namespace Celoxis.Api.Models;

public class CeloxisUser : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("lastAccessed")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastAccessed { get; set; }

    [JsonPropertyName("workCalendar")]
    public string WorkCalendar { get; set; }

    [JsonPropertyName("admin")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Admin { get; set; }

    [JsonPropertyName("accessType")]
    public string AccessType { get; set; }

    [JsonPropertyName("reportingManager")]
    public string ReportingManager { get; set; }

    [JsonPropertyName("workspace")]
    public string Workspace { get; set; }

    [JsonPropertyName("billRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BillRate { get; set; }

    [JsonPropertyName("costRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? CostRate { get; set; }

    [JsonPropertyName("roles")]
    public string Roles { get; set; }

    [JsonPropertyName("availableFrom")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? AvailableFrom { get; set; }

    [JsonPropertyName("availableTo")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? AvailableTo { get; set; }

    [JsonPropertyName("primaryJobRole")]
    public string PrimaryJobRole { get; set; }
}
