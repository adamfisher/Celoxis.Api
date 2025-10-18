using Celoxis.Api.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public class CeloxisTimeEntry : CeloxisModel
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
    [JsonConverter(typeof(AssociationConverter<CeloxisUser>))]
    public Association<CeloxisUser> User { get; set; }

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
    public decimal Hours { get; set; }

    [JsonPropertyName("comments")]
    public string Comments { get; set; }

    [JsonPropertyName("timeCode")]
    public string TimeCode { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("workItem")]
    [JsonConverter(typeof(AssociationConverter<CeloxisTask>))]
    public Association<CeloxisTask> WorkItem { get; set; }

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
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsBillable { get; set; }

    [JsonPropertyName("billRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BillRate { get; set; }

    [JsonPropertyName("revenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Revenue { get; set; }

    [JsonPropertyName("isCostable")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsCostable { get; set; }

    [JsonPropertyName("costRate")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? CostRate { get; set; }

    [JsonPropertyName("cost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Cost { get; set; }

    [JsonPropertyName("externalKey")]
    public string ExternalKey { get; set; }

    [JsonPropertyName("timeType")]
    public string TimeType { get; set; }

    [JsonPropertyName("task")]
    [JsonConverter(typeof(AssociationConverter<CeloxisTask>))]
    public Association<CeloxisTask> Task { get; set; }

    [JsonPropertyName("app")]
    [JsonConverter(typeof(AssociationConverter<CeloxisApp>))]
    public Association<CeloxisApp> App { get; set; }

    [JsonPropertyName("project")]
    [JsonConverter(typeof(AssociationConverter<CeloxisProject>))]
    public Association<CeloxisProject> Project { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTimeEntryAssociations Associations { get; set; }
}