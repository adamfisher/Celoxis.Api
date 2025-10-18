using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Celoxis.Api.Models;

namespace Celoxis.Api.Serialization.Converters;

public class AssociationConverter<T> : JsonConverter<Association<T>>
{
    public override Association<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new Association<T> { Url = reader.GetString() };
        }
        
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;
            
            if (root.TryGetProperty("data", out var dataElement))
            {
                var data = JsonSerializer.Deserialize<T>(dataElement.GetRawText(), options);
                return new Association<T> { Data = data };
            }
            
            var directData = JsonSerializer.Deserialize<T>(root.GetRawText(), options);
            return new Association<T> { Data = directData };
        }
        
        return new Association<T>();
    }

    public override void Write(Utf8JsonWriter writer, Association<T> value, JsonSerializerOptions options)
    {
        if (value.IsExpanded && value.Data != null)
        {
            JsonSerializer.Serialize(writer, value.Data, options);
        }
        else if (value.Url != null)
        {
            writer.WriteStringValue(value.Url);
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}