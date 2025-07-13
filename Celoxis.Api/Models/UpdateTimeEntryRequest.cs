using System;

namespace Celoxis.Api.Models;

public class UpdateTimeEntryRequest : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string? App { get; set; }
    public string? Task { get; set; }
    public string? User { get; set; }
    public decimal? BillRate { get; set; }
    public bool? IsBillable { get; set; }
    public string? Comments { get; set; }
    public decimal? CostRate { get; set; }
    public bool? IsCostable { get; set; }
    public DateTimeOffset? Date { get; set; }
    public string? ExternalKey { get; set; }
    public string? Hours { get; set; }
    public DateTimeOffset? InvoicedOn { get; set; }
    public string? State { get; set; }
    public string? TimeCode { get; set; }
}
