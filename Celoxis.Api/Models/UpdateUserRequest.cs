namespace Celoxis.Api.Models;

public class UpdateUserRequest : CeloxisModel
{
    public string Id { get; set; } = string.Empty;
    public string? Dashboards { get; set; }
    public string? Locale { get; set; }
    public string? Password { get; set; }
    public bool? Admin { get; set; }
    public decimal? BillRate { get; set; }
    public decimal? CostRate { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? PrimaryJobRole { get; set; }
    public string? ReportingManager { get; set; }
    public string? Username { get; set; }
    public string? WorkCalendar { get; set; }
}
