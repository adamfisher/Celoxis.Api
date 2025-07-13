using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CeloxisTaskUpdate : CeloxisModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("taskUpdateBy")]
    public string TaskUpdateBy { get; set; }

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("comments")]
    public string Comments { get; set; }

    [JsonPropertyName("percentComplete")]
    public string PercentComplete { get; set; }

    [JsonPropertyName("actualStart")]
    public DateTimeOffset? ActualStart { get; set; }

    [JsonPropertyName("actualFinish")]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("task")]
    public DataFieldWrapper<CeloxisTask> Task { get; set; }

    [JsonPropertyName("project")]
    public DataFieldWrapper<CeloxisProject> Project { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTaskUpdateAssociations Associations { get; set; }
}