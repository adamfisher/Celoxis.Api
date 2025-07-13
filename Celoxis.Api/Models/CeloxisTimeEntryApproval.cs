using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTimeEntryApproval
{
    [JsonPropertyName("approved")]
    public bool Approved { get; set; }

    [JsonPropertyName("by")]
    public string By { get; set; }

    [JsonPropertyName("date")]
    public DateTimeOffset? Date { get; set; }
}