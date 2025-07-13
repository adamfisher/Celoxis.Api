using System;

namespace Celoxis.Api.Models;

public class CreateAppRequest : CeloxisModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string App { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string? Requestor { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public string? Assignee { get; set; }
}
