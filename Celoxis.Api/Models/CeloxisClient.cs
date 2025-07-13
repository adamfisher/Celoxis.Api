using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CeloxisClient : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("custom_cf662079")]
    public object CustomCf662079 { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("parent")]
    public object Parent { get; set; }

    [JsonPropertyName("hierarchy")]
    public string Hierarchy { get; set; }

    [JsonPropertyName("root")]
    public string Root { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("lastAccessed")]
    public object LastAccessed { get; set; }
}
