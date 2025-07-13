using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CeloxisUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("lastAccessed")]
    [JsonConverter(typeof(FlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastAccessed { get; set; }

    [JsonPropertyName("workCalendar")]
    public string WorkCalendar { get; set; }

    [JsonPropertyName("admin")]
    public string Admin { get; set; }

    [JsonPropertyName("accessType")]
    public string AccessType { get; set; }

    [JsonPropertyName("reportingManager")]
    public string ReportingManager { get; set; }

    [JsonPropertyName("workspace")]
    public string Workspace { get; set; }

    [JsonPropertyName("billRate")]
    public string BillRate { get; set; }

    [JsonPropertyName("costRate")]
    public string CostRate { get; set; }

    [JsonPropertyName("roles")]
    public string Roles { get; set; }

    [JsonPropertyName("availableFrom")]
    public object AvailableFrom { get; set; }

    [JsonPropertyName("availableTo")]
    public object AvailableTo { get; set; }

    [JsonPropertyName("primaryJobRole")]
    public string PrimaryJobRole { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object> AdditionalProperties { get; set; } = new();
}
