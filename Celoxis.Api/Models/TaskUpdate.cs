using System;

namespace Celoxis.Api.Models;

public class TaskUpdate : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public object Task { get; set; } = string.Empty;
    public string Update { get; set; } = string.Empty;
    public DateTimeOffset? Created { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
