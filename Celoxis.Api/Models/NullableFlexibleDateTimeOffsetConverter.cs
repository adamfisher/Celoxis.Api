using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Celoxis.Api.Models;

public class NullableFlexibleDateTimeOffsetConverter : JsonConverter<DateTimeOffset?>
{
    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        string raw = reader.GetString();
        
        if (string.IsNullOrWhiteSpace(raw))
            return null;

        if (Regex.IsMatch(raw, @"[+-]\d{4}$"))
        {
            raw = raw.Insert(raw.Length - 2, ":");
        }

        return DateTimeOffset.Parse(raw, null, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString("o"));
        else
            writer.WriteNullValue();
    }
}
