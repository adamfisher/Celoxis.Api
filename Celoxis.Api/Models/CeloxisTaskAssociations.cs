using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTaskAssociations
{
    [JsonPropertyName("project")]
    public Uri Project { get; set; }

    [JsonPropertyName("parent")]
    public Uri Parent { get; set; }

    [JsonPropertyName("assignments")]
    public Uri Assignments { get; set; }

    [JsonPropertyName("updates")]
    public Uri Updates { get; set; }
}