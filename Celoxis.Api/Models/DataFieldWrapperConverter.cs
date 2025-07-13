using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class DataFieldWrapperConverter<T> : JsonConverter<DataFieldWrapper<T>>
{
    public override DataFieldWrapper<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException("Expected StartObject token");

        var wrapper = new DataFieldWrapper<T>();
        
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();

                if (propertyName == "data")
                {
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        wrapper.StringValue = reader.GetString();
                    }
                    else
                    {
                        wrapper.Data = JsonSerializer.Deserialize<T>(ref reader, options);
                    }
                }
            }
        }

        return wrapper;
    }

    public override void Write(Utf8JsonWriter writer, DataFieldWrapper<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        
        if (value.IsString)
        {
            writer.WriteString("data", value.StringValue);
        }
        else
        {
            writer.WritePropertyName("data");
            JsonSerializer.Serialize(writer, value.Data, options);
        }
        
        writer.WriteEndObject();
    }
}
