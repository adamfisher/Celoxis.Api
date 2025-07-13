using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisProjectAssociations
{
    [JsonPropertyName("manager")]
    public Uri Manager { get; set; }

    [JsonPropertyName("clients")]
    public Uri Clients { get; set; }

    [JsonPropertyName("client")]
    public Uri Client { get; set; }
}