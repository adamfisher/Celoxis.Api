using System;
using System.Globalization;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisProjectFaker : Faker<CeloxisProject>
{
    public CeloxisProjectFaker() =>
        Rules((f, p) =>
        {
            p.Id = f.Random.Number(800000, 999999).ToString();
            p.Url = new Uri($"https://app.celoxis.com/psa/projects/{p.Id}");
            p.Manager = new DataFieldWrapper<CeloxisManager>(f.Person.FullName);
            p.Creator = f.Person.FullName;
            p.ManagersAll = f.Person.FullName;
            p.Client = new DataFieldWrapper<Client>(f.Person.FullName);

            p.Name = f.Company.CompanyName() + " - Project";

            p.Description = f.Random.Bool(0.3f) ? f.Lorem.Paragraph() : "";
            p.Code = f.Random.Bool(0.4f) ? f.Random.AlphaNumeric(8).ToUpper() : "";
            
            var created = f.Date.Past(2);
            p.Created = created;
            p.LastModified = f.Date.Between(created, DateTime.Now);
            
            var plannedStart = f.Date.Between(created.AddDays(-30), DateTime.Now.AddDays(180));
            p.PlannedStart = plannedStart;
            SetDateRelatedFields(p, plannedStart);
            
            p.Deadline = f.Random.Bool(0.4f) ? f.Date.Future(6, plannedStart) : null;
            if (p.Deadline.HasValue)
            {
                SetDeadlineRelatedFields(p, p.Deadline.Value.DateTime);
            }
            
            var plannedFinish = f.Random.Bool(0.8f) ? 
                f.Date.Between(plannedStart.AddDays(7), plannedStart.AddDays(365)) : 
                (DateTimeOffset?)null;
            
            if (plannedFinish.HasValue)
            {
                p.PlannedFinish = plannedFinish.Value;
                SetPlannedFinishRelatedFields(p, plannedFinish.Value);
                
                p.ProjectedFinish = f.Date.Between(plannedFinish.Value.AddDays(-30).Date, plannedFinish.Value.AddDays(90).Date);
            }
            
            p.ActualFinish = f.Random.Bool(0.3f) ? f.Date.Recent(180) : null;
            
            p.Priority = f.PickRandom(FakerConstants.Priorities);
            p.ActualPercentComplete = f.Random.Number(0, 100).ToString();
            p.State = f.PickRandom(FakerConstants.ProjectStates);
            p.ScheduleType = f.PickRandom(FakerConstants.ScheduleTypes);
            
            SetBaselineFields(p, f);
            
            p.ScheduleHealth = f.PickRandom(FakerConstants.ScheduleHealths);
            p.BudgetHealth = f.PickRandom(FakerConstants.BudgetHealths);
            p.Type = f.PickRandom(FakerConstants.ProjectTypes);
            p.Workspace = f.PickRandom(FakerConstants.Workspaces);
            p.WorkCalendar = "Default";
            
            SetFinancialFields(p, f);
            
            p.Alignment = f.Random.Bool(0.3f) ? f.Random.Number(0, 100) : null;
            p.Benefit = f.Random.Bool(0.3f) ? f.Random.Number(0, 100) : null;
            p.Risk = "NORMAL";
            
            SetPerformanceMetrics(p, f);
            
            p.PlannedPercentComplete = f.Random.Number(0, 100).ToString();
            p.Acwp = f.Random.Number(0, 50000).ToString();
            
            var teamMembers = f.PickRandom([f.Person.FullName, f.Person.FullName, f.Person.FullName, f.Person.FullName], 2);
            p.Team = string.Join(", ", teamMembers);
            
            p.ProjectAssociations = new ProjectAssociations
            {
                Manager = new Uri($"https://app.celoxis.com/psa//api/v2/projects/{p.Id}/manager"),
                Clients = new Uri($"https://app.celoxis.com/psa//api/v2/projects/{p.Id}/clients"),
                Client = new Uri($"https://app.celoxis.com/psa//api/v2/projects/{p.Id}/client")
            };
        });

    private static void SetDateRelatedFields(CeloxisProject p, DateTimeOffset date)
    {
        var calendar = CultureInfo.InvariantCulture.Calendar;
        var weekOfYear = calendar.GetWeekOfYear(date.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        
        p.PlannedStartWeek = $"{weekOfYear:D2}-{date.Year}";
        p.PlannedStartWeekDate = date.AddDays(-(int)date.DayOfWeek + 1);
        p.PlannedStartMonth = date.ToString("yyyy-MM");
        p.PlannedStartQuarter = $"Q{(date.Month - 1) / 3 + 1} {date.Year}";
        p.PlannedStartYear = date.Year.ToString();
        p.PlannedStartFiscalYear = date.Year.ToString();
    }
    
    private static void SetDeadlineRelatedFields(CeloxisProject p, DateTime deadline)
    {
        var calendar = CultureInfo.InvariantCulture.Calendar;
        var weekOfYear = calendar.GetWeekOfYear(deadline, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        
        p.DeadlineWeek = $"{weekOfYear:D2}-{deadline.Year}";
        p.DeadlineWeekDate = deadline.AddDays(-(int)deadline.DayOfWeek + 1).ToString("yyyy-MM-dd");
        p.DeadlineMonth = deadline.ToString("yyyy-MM");
        p.DeadlineQuarter = $"Q{(deadline.Month - 1) / 3 + 1} {deadline.Year}";
        p.DeadlineYear = deadline.Year.ToString();
        p.DeadlineFiscalYear = deadline.Year.ToString();
    }
    
    private static void SetPlannedFinishRelatedFields(CeloxisProject p, DateTimeOffset finish)
    {
        var calendar = CultureInfo.InvariantCulture.Calendar;
        var weekOfYear = calendar.GetWeekOfYear(finish.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        
        p.PlannedFinishWeek = $"{weekOfYear:D2}-{finish.Year}";
        p.PlannedFinishWeekDate = finish.AddDays(-(int)finish.DayOfWeek + 1);
        p.PlannedFinishMonth = finish.ToString("yyyy-MM");
        p.PlannedFinishQuarter = $"Q{(finish.Month - 1) / 3 + 1} {finish.Year}";
        p.PlannedFinishYear = finish.Year.ToString();
        p.PlannedFinishFiscalYear = finish.Year.ToString();
    }
    
    private static void SetBaselineFields(CeloxisProject p, Faker f)
    {
        var hasBaseline = f.Random.Bool(0.2f);
        
        p.BaselineStart = hasBaseline ? f.Date.Past().ToString("yyyy-MM-dd") : null;
        p.BaselineStartVariance = hasBaseline ? $"{f.Random.Number(-5, 5)} Days" : "";
        p.BaselineDeadline = hasBaseline && f.Random.Bool(0.5f) ? f.Date.Future().ToString("yyyy-MM-dd") : null;
        p.BaselineDeadlineVariance = "";
        p.BaselineFinish = hasBaseline ? f.Date.Future().ToString("yyyy-MM-dd") : null;
        p.BaselineFinishVariance = hasBaseline ? $"{f.Random.Number(-10, 10)} Days" : "";
        p.BaselineCost = hasBaseline ? f.Random.Number(1000, 100000).ToString() : null;
        p.BaselineBudget = hasBaseline ? f.Random.Number(0, 50000).ToString() : null;
        p.BaselinePercentComplete = hasBaseline ? f.Random.Number(0, 100).ToString() : "0";
        p.BaselineEffort = hasBaseline ? f.Random.Number(10, 1000).ToString() : null;
        p.BaselineEffortVariance = f.Random.Number(0, 100).ToString();
    }
    
    private static void SetFinancialFields(CeloxisProject p, Faker f)
    {
        p.Budget = f.Random.Number(0, 100000).ToString();
        p.BillingType = f.PickRandom(FakerConstants.BillingTypes);
        p.PlannedRevenue = "0";
        p.PlannedLaborRevenue = "0";
        p.FixedPrice = "0";
        
        var plannedEffort = f.Random.Number(0, 3000);
        p.PlannedEffort = plannedEffort.ToString();
        
        var actualHours = f.Random.Number(0, plannedEffort);
        p.ActualLaborHours = actualHours.ToString();
        p.EffortVariance = (plannedEffort - actualHours).ToString();
        
        p.ActualBillableLaborHours = f.Random.Number(0, actualHours).ToString();
        p.ActualNonBillableLaborHours = (actualHours - int.Parse(p.ActualBillableLaborHours)).ToString();
        p.RemainingEffort = f.Random.Number(0, plannedEffort).ToString();
        
        var plannedCost = plannedEffort * f.Random.Number(80, 150);
        p.PlannedCost = plannedCost.ToString();
        
        var actualCost = actualHours * f.Random.Number(80, 150);
        p.ActualCost = actualCost.ToString();
        
        p.InvoicedLaborHours = "0";
        p.InvoicedRevenue = "0";
        p.UninvoicedRevenue = f.Random.Number(0, 5000).ToString();
        p.InvoicedLaborAmount = "0";
        p.InvoicedExpense = "0";
        
        p.PlannedLaborCost = plannedCost.ToString();
        p.ActualLaborCost = actualCost.ToString();
        p.PlannedNonLaborCost = "0";
        p.ActualNonLaborCost = "0";
        p.PlannedFixedCost = "0";
        p.ActualFixedCost = "0";
        
        p.PlannedProfit = (-plannedCost).ToString();
        p.PlannedMargin = "0";
        
        var revenue = int.Parse(p.UninvoicedRevenue);
        p.ActualProfit = (revenue - actualCost).ToString();
        p.ActualMargin = revenue > 0 ? ((revenue - actualCost) / (decimal)revenue * 100).ToString("F3") : "0";
        
        p.ProjectedCost = f.Random.Number(0, plannedCost * 2).ToString();
        p.ProjectedLaborHours = f.Random.Number(0, plannedEffort * 2).ToString();
        p.ActualRevenue = p.UninvoicedRevenue;
        p.ActualNonBillableLaborAmount = "0";
        p.ActualNonBillableExpense = "0";
    }
    
    private static void SetPerformanceMetrics(CeloxisProject p, Faker f)
    {
        var hasMetrics = f.Random.Bool(0.3f);
        
        p.Cpi = hasMetrics ? f.Random.Decimal(0.5m, 1.5m).ToString("F3") : null;
        p.Spi = hasMetrics ? f.Random.Decimal(0.8m, 1.2m).ToString() : null;
        p.BaselineCostVariance = hasMetrics ? f.Random.Number(-10000, 5000).ToString() : null;
        p.BaselineScheduleVariance = hasMetrics ? f.Random.Number(-30, 10).ToString() : null;
        p.Bcwp = hasMetrics ? f.Random.Number(1000, 50000).ToString() : null;
        p.Bcws = hasMetrics ? f.Random.Number(1000, 50000).ToString() : null;
    }
}