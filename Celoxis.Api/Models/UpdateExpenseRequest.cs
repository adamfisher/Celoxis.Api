using System;

namespace Celoxis.Api.Models;

public class UpdateExpenseRequest : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? Date { get; set; }
    public decimal? Amount { get; set; }
    public string? Category { get; set; }
    public string? Project { get; set; }
    public string? Task { get; set; }
    public string? User { get; set; }
    public string? State { get; set; }
}
