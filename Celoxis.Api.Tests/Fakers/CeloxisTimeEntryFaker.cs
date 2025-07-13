using System;
using System.Globalization;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisTimeEntryFaker : Faker<CeloxisTimeEntry>
{
    public CeloxisTimeEntryFaker() =>
        Rules((f, e) =>
        {
            e.Id = f.Random.Number(100000, 999999).ToString();
            e.Url = $"https://app.celoxis.com/psa/timeentries/{e.Id}";
            e.AccountingCode = "";
            e.Created = f.Date.Recent(7);
            e.Date = f.Date.Recent(30);
            
            var weekOfYear = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(
                e.Date.Value.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            e.DateWeek = $"W{weekOfYear:D2}-{e.Date:yyyy}";
            e.DateMonth = e.Date.Value.ToString("yyyy-MM");
            e.DateQuarter = $"Q{(e.Date.Value.Month - 1) / 3 + 1} {e.Date.Value.Year}";
            e.DateYear = e.Date.Value.Year.ToString();
            e.DateFiscalYear = e.Date.Value.Year.ToString();
            e.LastModified = e.Created;
            e.Hours = Math.Round(f.Random.Decimal(0.5m, 8m), 1);
            e.Comments = f.Lorem.Sentence();
            e.TimeCode = f.PickRandom("Default", "Meeting", "Development", "Testing");
            e.State = f.PickRandom(States.TimeEntry.Saved, States.TimeEntry.PendingApproval, States.TimeEntry.Approved);
            e.WorkItem = $"https://app.celoxis.com/psa/api/v2/timeEntries/{e.Id}/workItem";
            e.YearWeek = $"{e.Date:yyyy}-W{weekOfYear:D2}";
            e.ApprovedOn = f.Random.Bool(0.3f) ? f.Date.Recent(2) : null;
            e.InvoicedOn = null;
            e.Approvals = "";
            e.IsBillable = f.Random.Bool(0.8f) ? "Yes" : "No";
            e.BillRate = f.Random.Number(100, 300);
            e.Revenue = e.IsBillable == "Yes" ? e.Hours * e.BillRate : 0;
            e.IsCostable = "Yes";
            e.CostRate = f.Random.Number(50, 150);
            e.Cost = e.Hours * e.CostRate;
            e.User = $"https://app.celoxis.com/psa/api/v2/timeEntries/{e.Id}/user";
            e.Approver = $"https://app.celoxis.com/psa/api/v2/timeEntries/{e.Id}/approver";
            e.Project = $"https://app.celoxis.com/psa/api/v2/timeEntries/{e.Id}/project";
        });
}
