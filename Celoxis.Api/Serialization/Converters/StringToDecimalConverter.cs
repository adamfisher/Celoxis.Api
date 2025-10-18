using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Serialization.Converters;

public class StringToDecimalConverter : JsonConverter<decimal?>
{
    public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value)) return null;
            return decimal.TryParse(value, out var result) ? result : null;
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetDecimal();
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.ToString());
        else
            writer.WriteStringValue("");
    }
}