using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CeloxisTimeEntry
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("quickBooks Online Id")]
    public string QuickBooksOnlineId { get; set; }

    [JsonPropertyName("xaTs")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? XaTs { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }

    [JsonPropertyName("creator")]
    public string Creator { get; set; }

    [JsonPropertyName("invoicedBy")]
    public string InvoicedBy { get; set; }

    [JsonPropertyName("accountingCode")]
    public string AccountingCode { get; set; }

    [JsonPropertyName("created")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("date")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Date { get; set; }

    [JsonPropertyName("dateWeek")]
    public string DateWeek { get; set; }

    [JsonPropertyName("dateWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? DateWeekDate { get; set; }

    [JsonPropertyName("dateMonth")]
    public string DateMonth { get; set; }

    [JsonPropertyName("dateQuarter")]
    public string DateQuarter { get; set; }

    [JsonPropertyName("dateYear")]
    public string DateYear { get; set; }

    [JsonPropertyName("dateFiscalYear")]
    public string DateFiscalYear { get; set; }

    [JsonPropertyName("lastModified")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastModified { get; set; }

    [JsonPropertyName("hours")]
    public double Hours { get; set; }

    [JsonPropertyName("comments")]
    public string Comments { get; set; }

    [JsonPropertyName("timeCode")]
    public string TimeCode { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("workItem")]
    public string WorkItem { get; set; }

    [JsonPropertyName("yearWeek")]
    public string YearWeek { get; set; }

    [JsonPropertyName("approvedOn")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ApprovedOn { get; set; }

    [JsonPropertyName("invoicedOn")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? InvoicedOn { get; set; }

    [JsonPropertyName("approvals")]
    public List<CeloxisTimeEntryApproval> Approvals { get; set; }

    [JsonPropertyName("isBillable")]
    public string IsBillable { get; set; }

    [JsonPropertyName("billRate")]
    public string BillRate { get; set; }

    [JsonPropertyName("revenue")]
    public string Revenue { get; set; }

    [JsonPropertyName("isCostable")]
    public string IsCostable { get; set; }

    [JsonPropertyName("costRate")]
    public string CostRate { get; set; }

    [JsonPropertyName("cost")]
    public string Cost { get; set; }

    [JsonPropertyName("externalKey")]
    public string ExternalKey { get; set; }

    [JsonPropertyName("timeType")]
    public string TimeType { get; set; }

    [JsonPropertyName("task")]
    public string Task { get; set; }

    [JsonPropertyName("app")]
    public string App { get; set; }

    [JsonPropertyName("project")]
    public string Project { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTimeEntryAssociations Associations { get; set; }

    [JsonExtensionData]
    public Dictionary<string, object> AdditionalProperties { get; set; } = new();
}
