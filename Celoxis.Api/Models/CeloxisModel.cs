using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public abstract class CeloxisModel
{
    [JsonExtensionData]
    public Dictionary<string, object> OtherFields { get; set; } = new();
}