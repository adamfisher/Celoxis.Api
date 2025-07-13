using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

[JsonConverter(typeof(DataFieldWrapperConverter<>))]
public partial class DataFieldWrapper<T>
{
    public DataFieldWrapper()
    {
    }

    public DataFieldWrapper(T data) => Data = data;

    public DataFieldWrapper(string data) => StringValue = data;

    [JsonPropertyName("data")]
    public T Data { get; set; }
    
    public string StringValue { get; set; }
    
    [JsonIgnore]
    public bool IsString => StringValue != null;
    
    [JsonIgnore]
    public bool HasData => Data != null || StringValue != null;
    
    public string AsString() => IsString ? StringValue : Data?.ToString();
    
    public T AsObject() => IsString ? default(T) : Data;
}
