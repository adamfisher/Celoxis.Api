using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Celoxis.Api.Models;

public class FlexibleDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string raw = reader.GetString();

        if (Regex.IsMatch(raw, @"[+-]\d{4}$")) // if offset is -0500
        {
            raw = raw.Insert(raw.Length - 2, ":"); // change to -05:00
        }

        return DateTimeOffset.Parse(raw, null, DateTimeStyles.RoundtripKind);
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("o")); // ISO 8601 with colon
    }
}