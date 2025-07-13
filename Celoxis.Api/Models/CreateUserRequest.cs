namespace Celoxis.Api.Models;

public class CreateUserRequest : CeloxisModel
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string WorkCalendar { get; set; } = string.Empty;
    public decimal BillRate { get; set; }
    public decimal CostRate { get; set; }
    public string Roles { get; set; } = string.Empty;
    public string? ReportingManager { get; set; }
    public string? Phone { get; set; }
    public bool? Admin { get; set; }
    public string? PrimaryJobRole { get; set; }
    public string? Password { get; set; }
}
