using System;

namespace Celoxis.Api.Models;

public class UpdateAppRequest : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? State { get; set; }
    public string? Priority { get; set; }
    public string? Assignee { get; set; }
    public DateTimeOffset? DueDate { get; set; }
    public string? LastUpdate { get; set; }
}
