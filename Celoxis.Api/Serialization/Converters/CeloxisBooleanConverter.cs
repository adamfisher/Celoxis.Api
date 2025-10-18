using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Serialization.Converters;

public class CeloxisBooleanConverter : JsonConverter<bool?>
{
    public override bool HandleNull => true;

    public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();
            return value?.ToLowerInvariant() switch
            {
                "yes" => true,
                "no" => false,
                "true" => true,
                "false" => false,
                _ => null
            };
        }
        return reader.TokenType == JsonTokenType.True;
    }

    public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteStringValue("");
        }
        else
        {
            writer.WriteStringValue(value == true ? "Yes" : "No");
        }
    }
}