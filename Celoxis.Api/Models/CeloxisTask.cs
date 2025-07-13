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
    public DateTimeOffset? PlannedStart { get; set; }

    [JsonPropertyName("plannedStartWeek")]
    public string PlannedStartWeek { get; set; }

    [JsonPropertyName("plannedStartWeekDate")]
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
    public DateTimeOffset? PlannedFinish { get; set; }

    [JsonPropertyName("plannedFinishWeek")]
    public string PlannedFinishWeek { get; set; }

    [JsonPropertyName("plannedFinishWeekDate")]
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
    public string ActualStart { get; set; }

    [JsonPropertyName("actualStartWeek")]
    public string ActualStartWeek { get; set; }

    [JsonPropertyName("actualStartWeekDate")]
    public string ActualStartWeekDate { get; set; }

    [JsonPropertyName("actualStartMonth")]
    public string ActualStartMonth { get; set; }

    [JsonPropertyName("actualStartQuarter")]
    public string ActualStartQuarter { get; set; }

    [JsonPropertyName("actualStartYear")]
    public string ActualStartYear { get; set; }

    [JsonPropertyName("actualStartFiscalYear")]
    public string ActualStartFiscalYear { get; set; }

    [JsonPropertyName("actualFinish")]
    public DateTimeOffset? ActualFinish { get; set; }

    [JsonPropertyName("actualFinishWeek")]
    public string ActualFinishWeek { get; set; }

    [JsonPropertyName("actualFinishWeekDate")]
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
    public DateTimeOffset? ProjectedStart { get; set; }

    [JsonPropertyName("projectedStartWeek")]
    public string ProjectedStartWeek { get; set; }

    [JsonPropertyName("projectedStartWeekDate")]
    public string ProjectedStartWeekDate { get; set; }

    [JsonPropertyName("projectedStartMonth")]
    public string ProjectedStartMonth { get; set; }

    [JsonPropertyName("projectedStartQuarter")]
    public string ProjectedStartQuarter { get; set; }

    [JsonPropertyName("projectedStartYear")]
    public string ProjectedStartYear { get; set; }

    [JsonPropertyName("projectedStartFiscalYear")]
    public string ProjectedStartFiscalYear { get; set; }

    [JsonPropertyName("projectedFinish")]
    public DateTimeOffset? ProjectedFinish { get; set; }

    [JsonPropertyName("projectedFinishWeek")]
    public string ProjectedFinishWeek { get; set; }

    [JsonPropertyName("projectedFinishWeekDate")]
    public string ProjectedFinishWeekDate { get; set; }

    [JsonPropertyName("projectedFinishMonth")]
    public string ProjectedFinishMonth { get; set; }

    [JsonPropertyName("projectedFinishQuarter")]
    public string ProjectedFinishQuarter { get; set; }

    [JsonPropertyName("projectedFinishYear")]
    public string ProjectedFinishYear { get; set; }

    [JsonPropertyName("projectedFinishFiscalYear")]
    public string ProjectedFinishFiscalYear { get; set; }

    [JsonPropertyName("projectedCost")]
    public string ProjectedCost { get; set; }

    [JsonPropertyName("actualRevenue")]
    public string ActualRevenue { get; set; }

    [JsonPropertyName("plannedEffort")]
    public string PlannedEffort { get; set; }

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
    public string ConstraintDate { get; set; }

    [JsonPropertyName("priority")]
    public string Priority { get; set; }

    [JsonPropertyName("actualPercentComplete")]
    public string ActualPercentComplete { get; set; }

    [JsonPropertyName("remainingEffort")]
    public string RemainingEffort { get; set; }

    [JsonPropertyName("issueType")]
    public string IssueType { get; set; }

    [JsonPropertyName("intStatus")]
    public string IntStatus { get; set; }

    [JsonPropertyName("intKey")]
    public string IntKey { get; set; }

    [JsonPropertyName("sprintName")]
    public string SprintName { get; set; }

    [JsonPropertyName("sprintStart")]
    public string SprintStart { get; set; }

    [JsonPropertyName("sprintFinish")]
    public string SprintFinish { get; set; }

    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTimeOffset? LastModified { get; set; }

    [JsonPropertyName("lastUpdate")]
    public string LastUpdate { get; set; }

    [JsonPropertyName("allUpdates")]
    public string AllUpdates { get; set; }

    [JsonPropertyName("lastUpdated")]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonPropertyName("resources")]
    public string Resources { get; set; }

    [JsonPropertyName("scheduleHealth")]
    public string ScheduleHealth { get; set; }

    [JsonPropertyName("budgetHealth")]
    public string BudgetHealth { get; set; }

    [JsonPropertyName("plannedRevenue")]
    public string PlannedRevenue { get; set; }

    [JsonPropertyName("plannedLaborRevenue")]
    public string PlannedLaborRevenue { get; set; }

    [JsonPropertyName("budget")]
    public string Budget { get; set; }

    [JsonPropertyName("fixedPrice")]
    public string FixedPrice { get; set; }

    [JsonPropertyName("billingType")]
    public string BillingType { get; set; }

    [JsonPropertyName("plannedCost")]
    public string PlannedCost { get; set; }

    [JsonPropertyName("actualCost")]
    public string ActualCost { get; set; }

    [JsonPropertyName("actualLaborHours")]
    public string ActualLaborHours { get; set; }

    [JsonPropertyName("effortVariance")]
    public string EffortVariance { get; set; }

    [JsonPropertyName("myEstHours")]
    public string MyEstHours { get; set; }

    [JsonPropertyName("myActHours")]
    public string MyActHours { get; set; }

    [JsonPropertyName("invoicedLaborHours")]
    public string InvoicedLaborHours { get; set; }

    [JsonPropertyName("invoicedRevenue")]
    public string InvoicedRevenue { get; set; }

    [JsonPropertyName("invoicedLaborAmount")]
    public string InvoicedLaborAmount { get; set; }

    [JsonPropertyName("invoicedExpense")]
    public string InvoicedExpense { get; set; }

    [JsonPropertyName("actualBillableLaborHours")]
    public string ActualBillableLaborHours { get; set; }

    [JsonPropertyName("actualNonBillableLaborHours")]
    public string ActualNonBillableLaborHours { get; set; }

    [JsonPropertyName("plannedFixedCost")]
    public string PlannedFixedCost { get; set; }

    [JsonPropertyName("plannedNonLaborCost")]
    public string PlannedNonLaborCost { get; set; }

    [JsonPropertyName("plannedLaborCost")]
    public string PlannedLaborCost { get; set; }

    [JsonPropertyName("actualNonBillableLaborAmount")]
    public string ActualNonBillableLaborAmount { get; set; }

    [JsonPropertyName("actualBillableLaborAmount")]
    public string ActualBillableLaborAmount { get; set; }

    [JsonPropertyName("actualBillableExpense")]
    public string ActualBillableExpense { get; set; }

    [JsonPropertyName("actualNonBillableExpense")]
    public string ActualNonBillableExpense { get; set; }

    [JsonPropertyName("actualLaborCost")]
    public string ActualLaborCost { get; set; }

    [JsonPropertyName("actualNonLaborCost")]
    public string ActualNonLaborCost { get; set; }

    [JsonPropertyName("actualFixedCost")]
    public string ActualFixedCost { get; set; }

    [JsonPropertyName("projectedLaborHours")]
    public string ProjectedLaborHours { get; set; }

    [JsonPropertyName("attachments")]
    public string Attachments { get; set; }

    [JsonPropertyName("milestone")]
    public string Milestone { get; set; }

    [JsonPropertyName("isManuallyScheduled")]
    public string IsManuallyScheduled { get; set; }

    [JsonPropertyName("isTimeAllowed")]
    public string IsTimeAllowed { get; set; }

    [JsonPropertyName("critical")]
    public string Critical { get; set; }

    [JsonPropertyName("cpi")]
    public object Cpi { get; set; }

    [JsonPropertyName("spi")]
    public object Spi { get; set; }

    [JsonPropertyName("baselineCostVariance")]
    public object BaselineCostVariance { get; set; }

    [JsonPropertyName("baselineScheduleVariance")]
    public object BaselineScheduleVariance { get; set; }

    [JsonPropertyName("plannedPercentComplete")]
    public string PlannedPercentComplete { get; set; }

    [JsonPropertyName("bcwp")]
    public object Bcwp { get; set; }

    [JsonPropertyName("bcws")]
    public object Bcws { get; set; }

    [JsonPropertyName("acwp")]
    public string Acwp { get; set; }

    [JsonPropertyName("completedOn")]
    public DateTimeOffset? CompletedOn { get; set; }

    [JsonPropertyName("baselineStart")]
    public string BaselineStart { get; set; }

    [JsonPropertyName("baselineStartVariance")]
    public string BaselineStartVariance { get; set; }

    [JsonPropertyName("baselineFinish")]
    public string BaselineFinish { get; set; }

    [JsonPropertyName("baselineFinishVariance")]
    public string BaselineFinishVariance { get; set; }

    [JsonPropertyName("baselineCost")]
    public object BaselineCost { get; set; }

    [JsonPropertyName("baselineBudget")]
    public object BaselineBudget { get; set; }

    [JsonPropertyName("baselinePercentComplete")]
    public string BaselinePercentComplete { get; set; }

    [JsonPropertyName("baselineEffort")]
    public object BaselineEffort { get; set; }

    [JsonPropertyName("baselineEffortVariance")]
    public object BaselineEffortVariance { get; set; }

    [JsonPropertyName("showInTimeline")]
    public string ShowInTimeline { get; set; }

    [JsonPropertyName("externalKey")]
    public string ExternalKey { get; set; }

    [JsonPropertyName("checklist")]
    public string Checklist { get; set; }

    [JsonPropertyName("project")]
    public DataFieldWrapper<CeloxisProject> Project { get; set; }

    [JsonPropertyName("associations")]
    public CeloxisTaskAssociations Associations { get; set; }
}