using System;
using System.Text.Json.Serialization;
using Celoxis.Api.Serialization.Converters;

namespace Celoxis.Api.Models;

public partial class CeloxisProject : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("creator")]
    public string Creator { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("created")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("lastModified")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastModified { get; set; }

    [JsonPropertyName("plannedStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedStart { get; set; }

    [JsonPropertyName("plannedStartWeek")]
    public string PlannedStartWeek { get; set; }

    [JsonPropertyName("plannedStartWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedStartWeekDate { get; set; }

    [JsonPropertyName("plannedStartMonth")]
    public string PlannedStartMonth { get; set; }

    [JsonPropertyName("plannedStartQuarter")]
    public string PlannedStartQuarter { get; set; }

    [JsonPropertyName("plannedStartYear")]
    public string PlannedStartYear { get; set; }

    [JsonPropertyName("plannedStartFiscalYear")]
    public string PlannedStartFiscalYear { get; set; }

    [JsonPropertyName("deadline")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Deadline { get; set; }

    [JsonPropertyName("deadlineWeek")]
    public string DeadlineWeek { get; set; }

    [JsonPropertyName("deadlineWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? DeadlineWeekDate { get; set; }

    [JsonPropertyName("deadlineMonth")]
    public string DeadlineMonth { get; set; }

    [JsonPropertyName("deadlineQuarter")]
    public string DeadlineQuarter { get; set; }

    [JsonPropertyName("deadlineYear")]
    public string DeadlineYear { get; set; }

    [JsonPropertyName("deadlineFiscalYear")]
    public string DeadlineFiscalYear { get; set; }

    [JsonPropertyName("plannedFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedFinish { get; set; }

    [JsonPropertyName("plannedFinishWeek")]
    public string PlannedFinishWeek { get; set; }

    [JsonPropertyName("plannedFinishWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? PlannedFinishWeekDate { get; set; }

    [JsonPropertyName("plannedFinishMonth")]
    public string PlannedFinishMonth { get; set; }

    [JsonPropertyName("plannedFinishQuarter")]
    public string PlannedFinishQuarter { get; set; }

    [JsonPropertyName("plannedFinishYear")]
    public string PlannedFinishYear { get; set; }

    [JsonPropertyName("plannedFinishFiscalYear")]
    public string PlannedFinishFiscalYear { get; set; }

    [JsonPropertyName("projectedFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ProjectedFinish { get; set; }

    [JsonPropertyName("actualFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("priority")]
    public string Priority { get; set; }

    [JsonPropertyName("actualPercentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? ActualPercentComplete { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("scheduleType")]
    public string ScheduleType { get; set; }

    [JsonPropertyName("baselineStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? BaselineStart { get; set; }

    [JsonPropertyName("baselineStartVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineStartVariance { get; set; }

    [JsonPropertyName("baselineDeadline")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? BaselineDeadline { get; set; }

    [JsonPropertyName("baselineDeadlineVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineDeadlineVariance { get; set; }

    [JsonPropertyName("baselineFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? BaselineFinish { get; set; }

    [JsonPropertyName("baselineFinishVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineFinishVariance { get; set; }

    [JsonPropertyName("baselineCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineCost { get; set; }

    [JsonPropertyName("baselineBudget")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineBudget { get; set; }

    [JsonPropertyName("baselinePercentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? BaselinePercentComplete { get; set; }

    [JsonPropertyName("baselineEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineEffort { get; set; }

    [JsonPropertyName("baselineEffortVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineEffortVariance { get; set; }

    [JsonPropertyName("scheduleHealth")]
    public string ScheduleHealth { get; set; }

    [JsonPropertyName("budgetHealth")]
    public string BudgetHealth { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("managersAll")]
    public string ManagersAll { get; set; }

    [JsonPropertyName("workspace")]
    public string Workspace { get; set; }

    [JsonPropertyName("workCalendar")]
    public string WorkCalendar { get; set; }

    [JsonPropertyName("budget")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Budget { get; set; }

    [JsonPropertyName("billingType")]
    public string BillingType { get; set; }

    [JsonPropertyName("plannedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedRevenue { get; set; }

    [JsonPropertyName("plannedLaborRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedLaborRevenue { get; set; }

    [JsonPropertyName("fixedPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? FixedPrice { get; set; }

    [JsonPropertyName("plannedEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedEffort { get; set; }

    [JsonPropertyName("actualLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualLaborHours { get; set; }

    [JsonPropertyName("effortVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? EffortVariance { get; set; }

    [JsonPropertyName("actualBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualBillableLaborHours { get; set; }

    [JsonPropertyName("actualNonBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableLaborHours { get; set; }

    [JsonPropertyName("remainingEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? RemainingEffort { get; set; }

    [JsonPropertyName("plannedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedCost { get; set; }

    [JsonPropertyName("actualCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualCost { get; set; }

    [JsonPropertyName("invoicedLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedLaborHours { get; set; }

    [JsonPropertyName("invoicedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedRevenue { get; set; }

    [JsonPropertyName("uninvoicedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? UninvoicedRevenue { get; set; }

    [JsonPropertyName("invoicedLaborAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedLaborAmount { get; set; }

    [JsonPropertyName("invoicedExpense")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedExpense { get; set; }

    [JsonPropertyName("plannedLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedLaborCost { get; set; }

    [JsonPropertyName("actualLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualLaborCost { get; set; }

    [JsonPropertyName("plannedNonLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedNonLaborCost { get; set; }

    [JsonPropertyName("actualNonLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonLaborCost { get; set; }

    [JsonPropertyName("plannedFixedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedFixedCost { get; set; }

    [JsonPropertyName("actualFixedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualFixedCost { get; set; }

    [JsonPropertyName("plannedProfit")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedProfit { get; set; }

    [JsonPropertyName("plannedMargin")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedMargin { get; set; }

    [JsonPropertyName("actualProfit")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualProfit { get; set; }

    [JsonPropertyName("actualMargin")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualMargin { get; set; }

    [JsonPropertyName("projectedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ProjectedCost { get; set; }

    [JsonPropertyName("projectedLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ProjectedLaborHours { get; set; }

    [JsonPropertyName("actualRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualRevenue { get; set; }

    [JsonPropertyName("actualNonBillableLaborAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableLaborAmount { get; set; }

    [JsonPropertyName("actualNonBillableExpense")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableExpense { get; set; }

    [JsonPropertyName("alignment")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? Alignment { get; set; }

    [JsonPropertyName("benefit")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? Benefit { get; set; }

    [JsonPropertyName("risk")]
    public string Risk { get; set; }

    [JsonPropertyName("cpi")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Cpi { get; set; }

    [JsonPropertyName("spi")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Spi { get; set; }

    [JsonPropertyName("baselineCostVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineCostVariance { get; set; }

    [JsonPropertyName("baselineScheduleVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineScheduleVariance { get; set; }

    [JsonPropertyName("plannedPercentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? PlannedPercentComplete { get; set; }

    [JsonPropertyName("bcwp")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Bcwp { get; set; }

    [JsonPropertyName("bcws")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Bcws { get; set; }

    [JsonPropertyName("acwp")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Acwp { get; set; }

    [JsonPropertyName("team")]
    public string Team { get; set; }

    [JsonPropertyName("manager")]
    [JsonConverter(typeof(AssociationConverter<CeloxisManager>))]
    public Association<CeloxisManager> Manager { get; set; }

    [JsonPropertyName("client")]
    public string Client { get; set; }

    [JsonPropertyName("clients")]
    [JsonConverter(typeof(AssociationConverter<Client[]>))]
    public Association<Client[]> Clients { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisProjectAssociations Associations { get; set; }
}
