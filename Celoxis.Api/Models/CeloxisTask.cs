using Celoxis.Api.Serialization.Converters;
using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisTask : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("hierarchy")]
    public string Hierarchy { get; set; }

    [JsonPropertyName("topmostTask")]
    public string TopmostTask { get; set; }

    [JsonPropertyName("sN")]
    public string SN { get; set; }

    [JsonPropertyName("wbs")]
    public string Wbs { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }

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

    [JsonPropertyName("actualStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualStart { get; set; }

    [JsonPropertyName("actualStartWeek")]
    public string ActualStartWeek { get; set; }

    [JsonPropertyName("actualStartWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualStartWeekDate { get; set; }

    [JsonPropertyName("actualStartMonth")]
    public string ActualStartMonth { get; set; }

    [JsonPropertyName("actualStartQuarter")]
    public string ActualStartQuarter { get; set; }

    [JsonPropertyName("actualStartYear")]
    public string ActualStartYear { get; set; }

    [JsonPropertyName("actualStartFiscalYear")]
    public string ActualStartFiscalYear { get; set; }

    [JsonPropertyName("actualFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("actualFinishWeek")]
    public string ActualFinishWeek { get; set; }

    [JsonPropertyName("actualFinishWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ActualFinishWeekDate { get; set; }

    [JsonPropertyName("actualFinishMonth")]
    public string ActualFinishMonth { get; set; }

    [JsonPropertyName("actualFinishQuarter")]
    public string ActualFinishQuarter { get; set; }

    [JsonPropertyName("actualFinishYear")]
    public string ActualFinishYear { get; set; }

    [JsonPropertyName("actualFinishFiscalYear")]
    public string ActualFinishFiscalYear { get; set; }

    [JsonPropertyName("projectedStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ProjectedStart { get; set; }

    [JsonPropertyName("projectedStartWeek")]
    public string ProjectedStartWeek { get; set; }

    [JsonPropertyName("projectedStartWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ProjectedStartWeekDate { get; set; }

    [JsonPropertyName("projectedStartMonth")]
    public string ProjectedStartMonth { get; set; }

    [JsonPropertyName("projectedStartQuarter")]
    public string ProjectedStartQuarter { get; set; }

    [JsonPropertyName("projectedStartYear")]
    public string ProjectedStartYear { get; set; }

    [JsonPropertyName("projectedStartFiscalYear")]
    public string ProjectedStartFiscalYear { get; set; }

    [JsonPropertyName("projectedFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ProjectedFinish { get; set; }

    [JsonPropertyName("projectedFinishWeek")]
    public string ProjectedFinishWeek { get; set; }

    [JsonPropertyName("projectedFinishWeekDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ProjectedFinishWeekDate { get; set; }

    [JsonPropertyName("projectedFinishMonth")]
    public string ProjectedFinishMonth { get; set; }

    [JsonPropertyName("projectedFinishQuarter")]
    public string ProjectedFinishQuarter { get; set; }

    [JsonPropertyName("projectedFinishYear")]
    public string ProjectedFinishYear { get; set; }

    [JsonPropertyName("projectedFinishFiscalYear")]
    public string ProjectedFinishFiscalYear { get; set; }

    [JsonPropertyName("projectedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ProjectedCost { get; set; }

    [JsonPropertyName("actualRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualRevenue { get; set; }

    [JsonPropertyName("plannedEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedEffort { get; set; }

    [JsonPropertyName("scheduleType")]
    public string ScheduleType { get; set; }

    [JsonPropertyName("duration")]
    public string Duration { get; set; }

    [JsonPropertyName("predecessors")]
    public string Predecessors { get; set; }

    [JsonPropertyName("successors")]
    public string Successors { get; set; }

    [JsonPropertyName("successorsInterProject")]
    public string SuccessorsInterProject { get; set; }

    [JsonPropertyName("predecessorsInterProject")]
    public string PredecessorsInterProject { get; set; }

    [JsonPropertyName("constraintType")]
    public string ConstraintType { get; set; }

    [JsonPropertyName("constraintDate")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? ConstraintDate { get; set; }

    [JsonPropertyName("priority")]
    public string Priority { get; set; }

    [JsonPropertyName("actualPercentComplete")]
    [JsonConverter(typeof(StringToIntConverter))]
    public int? ActualPercentComplete { get; set; }

    [JsonPropertyName("remainingEffort")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? RemainingEffort { get; set; }

    [JsonPropertyName("issueType")]
    public string IssueType { get; set; }

    [JsonPropertyName("intStatus")]
    public string IntStatus { get; set; }

    [JsonPropertyName("intKey")]
    public string IntKey { get; set; }

    [JsonPropertyName("sprintName")]
    public string SprintName { get; set; }

    [JsonPropertyName("sprintStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? SprintStart { get; set; }

    [JsonPropertyName("sprintFinish")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? SprintFinish { get; set; }

    [JsonPropertyName("created")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("lastModified")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastModified { get; set; }

    [JsonPropertyName("lastUpdate")]
    public string LastUpdate { get; set; }

    [JsonPropertyName("allUpdates")]
    public string AllUpdates { get; set; }

    [JsonPropertyName("lastUpdated")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonPropertyName("resources")]
    public string Resources { get; set; }

    [JsonPropertyName("scheduleHealth")]
    public string ScheduleHealth { get; set; }

    [JsonPropertyName("budgetHealth")]
    public string BudgetHealth { get; set; }

    [JsonPropertyName("plannedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedRevenue { get; set; }

    [JsonPropertyName("plannedLaborRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedLaborRevenue { get; set; }

    [JsonPropertyName("budget")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? Budget { get; set; }

    [JsonPropertyName("fixedPrice")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? FixedPrice { get; set; }

    [JsonPropertyName("billingType")]
    public string BillingType { get; set; }

    [JsonPropertyName("plannedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedCost { get; set; }

    [JsonPropertyName("actualCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualCost { get; set; }

    [JsonPropertyName("actualLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualLaborHours { get; set; }

    [JsonPropertyName("effortVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? EffortVariance { get; set; }

    [JsonPropertyName("myEstHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? MyEstHours { get; set; }

    [JsonPropertyName("myActHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? MyActHours { get; set; }

    [JsonPropertyName("invoicedLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedLaborHours { get; set; }

    [JsonPropertyName("invoicedRevenue")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedRevenue { get; set; }

    [JsonPropertyName("invoicedLaborAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedLaborAmount { get; set; }

    [JsonPropertyName("invoicedExpense")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? InvoicedExpense { get; set; }

    [JsonPropertyName("actualBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualBillableLaborHours { get; set; }

    [JsonPropertyName("actualNonBillableLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableLaborHours { get; set; }

    [JsonPropertyName("plannedFixedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedFixedCost { get; set; }

    [JsonPropertyName("plannedNonLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedNonLaborCost { get; set; }

    [JsonPropertyName("plannedLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? PlannedLaborCost { get; set; }

    [JsonPropertyName("actualNonBillableLaborAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableLaborAmount { get; set; }

    [JsonPropertyName("actualBillableLaborAmount")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualBillableLaborAmount { get; set; }

    [JsonPropertyName("actualBillableExpense")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualBillableExpense { get; set; }

    [JsonPropertyName("actualNonBillableExpense")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonBillableExpense { get; set; }

    [JsonPropertyName("actualLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualLaborCost { get; set; }

    [JsonPropertyName("actualNonLaborCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualNonLaborCost { get; set; }

    [JsonPropertyName("actualFixedCost")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ActualFixedCost { get; set; }

    [JsonPropertyName("projectedLaborHours")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? ProjectedLaborHours { get; set; }

    [JsonPropertyName("attachments")]
    public string Attachments { get; set; }

    [JsonPropertyName("milestone")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Milestone { get; set; }

    [JsonPropertyName("isManuallyScheduled")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsManuallyScheduled { get; set; }

    [JsonPropertyName("isTimeAllowed")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? IsTimeAllowed { get; set; }

    [JsonPropertyName("critical")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? Critical { get; set; }

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

    [JsonPropertyName("completedOn")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? CompletedOn { get; set; }

    [JsonPropertyName("baselineStart")]
    [JsonConverter(typeof(NullableFlexibleDateTimeOffsetConverter))]
    public DateTimeOffset? BaselineStart { get; set; }

    [JsonPropertyName("baselineStartVariance")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal? BaselineStartVariance { get; set; }

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

    [JsonPropertyName("showInTimeline")]
    [JsonConverter(typeof(CeloxisBooleanConverter))]
    public bool? ShowInTimeline { get; set; }

    [JsonPropertyName("externalKey")]
    public string ExternalKey { get; set; }

    [JsonPropertyName("checklist")]
    public string Checklist { get; set; }

    [JsonPropertyName("project")]
    [JsonConverter(typeof(AssociationConverter<CeloxisProject>))]
    public Association<CeloxisProject> Project { get; set; }

    [JsonPropertyName("parent")]
    [JsonConverter(typeof(AssociationConverter<CeloxisTask>))]
    public Association<CeloxisTask> Parent { get; set; }

    [JsonPropertyName("assignments")]
    public Association<object[]> Assignments { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTaskAssociations Associations { get; set; }
}
