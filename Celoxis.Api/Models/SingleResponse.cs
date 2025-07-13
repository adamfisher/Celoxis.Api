using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class SingleResponse<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; } = default!;
}