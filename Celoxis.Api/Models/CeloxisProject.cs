using System;
using System.Text.Json.Serialization;

namespace Celoxis.Api.Models;

public partial class CeloxisProject : CeloxisModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("client")]
    public DataFieldWrapper<Client> Client { get; set; }

    [JsonPropertyName("manager")]
    public DataFieldWrapper<CeloxisManager> Manager { get; set; }

    [JsonPropertyName("creator")]
    public string Creator { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("created")]
    public DateTimeOffset? Created { get; set; }

    [JsonPropertyName("lastModified")]
    public DateTimeOffset? LastModified { get; set; }

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

    [JsonPropertyName("deadline")]
    public DateTimeOffset? Deadline { get; set; }

    [JsonPropertyName("deadlineWeek")]
    public string DeadlineWeek { get; set; }

    [JsonPropertyName("deadlineWeekDate")]
    public string DeadlineWeekDate { get; set; }

    [JsonPropertyName("deadlineMonth")]
    public string DeadlineMonth { get; set; }

    [JsonPropertyName("deadlineQuarter")]
    public string DeadlineQuarter { get; set; }

    [JsonPropertyName("deadlineYear")]
    public string DeadlineYear { get; set; }

    [JsonPropertyName("deadlineFiscalYear")]
    public string DeadlineFiscalYear { get; set; }

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

    [JsonPropertyName("projectedFinish")]
    public DateTimeOffset? ProjectedFinish { get; set; }

    [JsonPropertyName("actualFinish")]
    public object ActualFinish { get; set; }

    [JsonPropertyName("priority")]
    public string Priority { get; set; }

    [JsonPropertyName("actualPercentComplete")]
    public string ActualPercentComplete { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("scheduleType")]
    public string ScheduleType { get; set; }

    [JsonPropertyName("baselineStart")]
    public object BaselineStart { get; set; }

    [JsonPropertyName("baselineStartVariance")]
    public string BaselineStartVariance { get; set; }

    [JsonPropertyName("baselineDeadline")]
    public object BaselineDeadline { get; set; }

    [JsonPropertyName("baselineDeadlineVariance")]
    public string BaselineDeadlineVariance { get; set; }

    [JsonPropertyName("baselineFinish")]
    public object BaselineFinish { get; set; }

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
    public string BaselineEffortVariance { get; set; }

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
    public string Budget { get; set; }

    [JsonPropertyName("billingType")]
    public string BillingType { get; set; }

    [JsonPropertyName("plannedRevenue")]
    public string PlannedRevenue { get; set; }

    [JsonPropertyName("plannedLaborRevenue")]
    public string PlannedLaborRevenue { get; set; }

    [JsonPropertyName("fixedPrice")]
    public string FixedPrice { get; set; }

    [JsonPropertyName("plannedEffort")]
    public string PlannedEffort { get; set; }

    [JsonPropertyName("actualLaborHours")]
    public string ActualLaborHours { get; set; }

    [JsonPropertyName("effortVariance")]
    public string EffortVariance { get; set; }

    [JsonPropertyName("actualBillableLaborHours")]
    public string ActualBillableLaborHours { get; set; }

    [JsonPropertyName("actualNonBillableLaborHours")]
    public string ActualNonBillableLaborHours { get; set; }

    [JsonPropertyName("remainingEffort")]
    public string RemainingEffort { get; set; }

    [JsonPropertyName("plannedCost")]
    public string PlannedCost { get; set; }

    [JsonPropertyName("actualCost")]
    public string ActualCost { get; set; }

    [JsonPropertyName("invoicedLaborHours")]
    public string InvoicedLaborHours { get; set; }

    [JsonPropertyName("invoicedRevenue")]
    public string InvoicedRevenue { get; set; }

    [JsonPropertyName("uninvoicedRevenue")]
    public string UninvoicedRevenue { get; set; }

    [JsonPropertyName("invoicedLaborAmount")]
    public string InvoicedLaborAmount { get; set; }

    [JsonPropertyName("invoicedExpense")]
    public string InvoicedExpense { get; set; }

    [JsonPropertyName("plannedLaborCost")]
    public string PlannedLaborCost { get; set; }

    [JsonPropertyName("actualLaborCost")]
    public string ActualLaborCost { get; set; }

    [JsonPropertyName("plannedNonLaborCost")]
    public string PlannedNonLaborCost { get; set; }

    [JsonPropertyName("actualNonLaborCost")]
    public string ActualNonLaborCost { get; set; }

    [JsonPropertyName("plannedFixedCost")]
    public string PlannedFixedCost { get; set; }

    [JsonPropertyName("actualFixedCost")]
    public string ActualFixedCost { get; set; }

    [JsonPropertyName("plannedProfit")]
    public string PlannedProfit { get; set; }

    [JsonPropertyName("plannedMargin")]
    public string PlannedMargin { get; set; }

    [JsonPropertyName("actualProfit")]
    public string ActualProfit { get; set; }

    [JsonPropertyName("actualMargin")]
    public string ActualMargin { get; set; }

    [JsonPropertyName("projectedCost")]
    public string ProjectedCost { get; set; }

    [JsonPropertyName("projectedLaborHours")]
    public string ProjectedLaborHours { get; set; }

    [JsonPropertyName("actualRevenue")]
    public string ActualRevenue { get; set; }

    [JsonPropertyName("actualNonBillableLaborAmount")]
    public string ActualNonBillableLaborAmount { get; set; }

    [JsonPropertyName("actualNonBillableExpense")]
    public string ActualNonBillableExpense { get; set; }

    [JsonPropertyName("alignment")]
    public object Alignment { get; set; }

    [JsonPropertyName("benefit")]
    public object Benefit { get; set; }

    [JsonPropertyName("risk")]
    public string Risk { get; set; }

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

    [JsonPropertyName("team")]
    public string Team { get; set; }

    [JsonPropertyName("associations")]
    public ProjectAssociations ProjectAssociations { get; set; }
}