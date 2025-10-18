using System;
using System.Text.Json.Serialization;
using Celoxis.Api.Serialization.Converters;

namespace Celoxis.Api.Models;

public class CeloxisTaskUpdate : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("taskUpdateBy")]
    public string TaskUpdateBy { get; set; }

    [JsonPropertyName("date")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("comments")]
    public string Comments { get; set; }

    [JsonPropertyName("percentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? PercentComplete { get; set; }

    [JsonPropertyName("actualStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualStart { get; set; }

    [JsonPropertyName("actualFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("task")]
    [JsonConverter(typeof(AssociationConverter<CeloxisTask>))]
    public Association<CeloxisTask> Task { get; set; }

    [JsonPropertyName("project")]
    [JsonConverter(typeof(AssociationConverter<CeloxisProject>))]
    public Association<CeloxisProject> Project { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTaskUpdateAssociations Associations { get; set; }
}
