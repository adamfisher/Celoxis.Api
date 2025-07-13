using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTimeEntryAssociations
{
    [JsonPropertyName("user")]
    public Uri User { get; set; }

    [JsonPropertyName("approver")]
    public Uri Approver { get; set; }

    [JsonPropertyName("workItem")]
    public Uri WorkItem { get; set; }

    [JsonPropertyName("project")]
    public Uri Project { get; set; }
}