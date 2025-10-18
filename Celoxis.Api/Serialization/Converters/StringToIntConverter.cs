using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Serialization.Converters;

public class StringToIntConverter : JsonConverter<int?>
{
    public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return null;
            return int.TryParse(value, out var result) ? result : null;
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.ToString());
        else
            writer.WriteStringValue("");
    }
}