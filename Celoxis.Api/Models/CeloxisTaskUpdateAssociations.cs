using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTaskUpdateAssociations
{
    [JsonPropertyName("task")]
    public Uri Task { get; set; }

    [JsonPropertyName("project")]
    public Uri Project { get; set; }
}