using Celoxis.Api.Serialization.Converters;
using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

/// <summary>
/// Represents an app (bug, issue, etc.)
/// </summary>
public class CeloxisApp : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("app")]
    public string App { get; set; } = string.Empty;

    [JsonPropertyName("creator")]
    public string Creator { get; set; } = string.Empty;

    [JsonPropertyName("assignee")]
    public string Assignee { get; set; } = string.Empty;

    [JsonPropertyName("stateManager")]
    public string StateManager { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public string Age { get; set; } = string.Empty;

    [JsonPropertyName("requestor")]
    public string Requestor { get; set; } = string.Empty;

    [JsonPropertyName("workspace")]
    public string Workspace { get; set; } = string.Empty;

    [JsonPropertyName("dueDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? DueDate { get; set; }

    [JsonPropertyName("dueDateWeek")]
    public string DueDateWeek { get; set; } = string.Empty;

    [JsonPropertyName("dueDateWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? DueDateWeekDate { get; set; }

    [JsonPropertyName("dueDateMonth")]
    public string DueDateMonth { get; set; } = string.Empty;

    [JsonPropertyName("dueDateQuarter")]
    public string DueDateQuarter { get; set; } = string.Empty;

    [JsonPropertyName("dueDateYear")]
    public string DueDateYear { get; set; } = string.Empty;

    [JsonPropertyName("dueDateFiscalYear")]
    public string DueDateFiscalYear { get; set; } = string.Empty;

    [JsonPropertyName("created")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("createdWeek")]
    public string CreatedWeek { get; set; } = string.Empty;

    [JsonPropertyName("createdWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? CreatedWeekDate { get; set; }

    [JsonPropertyName("createdMonth")]
    public string CreatedMonth { get; set; } = string.Empty;

    [JsonPropertyName("createdQuarter")]
    public string CreatedQuarter { get; set; } = string.Empty;

    [JsonPropertyName("createdYear")]
    public string CreatedYear { get; set; } = string.Empty;

    [JsonPropertyName("createdFiscalYear")]
    public string CreatedFiscalYear { get; set; } = string.Empty;

    [JsonPropertyName("timeout")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Timeout { get; set; }

    [JsonPropertyName("completed")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Completed { get; set; }

    [JsonPropertyName("priority")]
    public string Priority { get; set; } = string.Empty;

    [JsonPropertyName("lastUpdate")]
    public string LastUpdate { get; set; } = string.Empty;

    [JsonPropertyName("lastUpdatedOn")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastUpdatedOn { get; set; }

    [JsonPropertyName("allUpdates")]
    public string AllUpdates { get; set; } = string.Empty;

    [JsonPropertyName("delayed")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Delayed { get; set; }

    [JsonPropertyName("requestorVisible")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? RequestorVisible { get; set; }

    [JsonPropertyName("clientVisible")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? ClientVisible { get; set; }

    [JsonPropertyName("open")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Open { get; set; }

    [JsonPropertyName("actualRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualRevenue { get; set; }

    [JsonPropertyName("actualCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualCost { get; set; }

    [JsonPropertyName("actualEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualEffort { get; set; }

    [JsonPropertyName("actualBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualBillableLaborHours { get; set; }

    [JsonPropertyName("actualNonBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableLaborHours { get; set; }

    [JsonPropertyName("invoicedLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedLaborHours { get; set; }

    [JsonPropertyName("invoicedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedRevenue { get; set; }

    [JsonPropertyName("checklist")]
    public string Checklist { get; set; } = string.Empty;

    [JsonPropertyName("project")]
    [JsonConverter(typeof(AssociationConverter<CeloxisProject>))]
    public Association<CeloxisProject> Project { get; set; }

    [JsonPropertyName("assignedTo")]
    [JsonConverter(typeof(AssociationConverter<CeloxisUser>))]
    public Association<CeloxisUser> AssignedTo { get; set; }

    [JsonPropertyName("associations")]
    public object Associations { get; set; }
}