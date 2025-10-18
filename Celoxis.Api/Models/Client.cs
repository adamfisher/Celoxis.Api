using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class Client : CeloxisModel
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

    [JsonPropertyName("parent")]
    public string Parent { get; set; }

    [JsonPropertyName("hierarchy")]
    public string Hierarchy { get; set; }

    [JsonPropertyName("root")]
    public string Root { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("lastAccessed")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastAccessed { get; set; }
}
