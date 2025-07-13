namespace Celoxis.Api.Models;

public class CreateTaskUpdateRequest : CeloxisModel
{
    public string Task { get; set; } = string.Empty;
    public string Update { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
}
