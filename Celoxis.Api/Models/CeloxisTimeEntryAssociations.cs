using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTimeEntryAssociations
{
    [JsonPropertyName("user")]
    public string User { get; set; }

    [JsonPropertyName("approver")]
    public string Approver { get; set; }

    [JsonPropertyName("workItem")]
    public string WorkItem { get; set; }

    [JsonPropertyName("project")]
    public string Project { get; set; }
}
