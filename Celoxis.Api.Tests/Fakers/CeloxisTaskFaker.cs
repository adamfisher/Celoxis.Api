using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisTaskFaker : Faker<CeloxisTask>
{
    public CeloxisTaskFaker() =>
        Rules((f, t) =>
        {
            t.Id = f.Random.Number(10000, 99999).ToString();
            t.Url = new Uri($"https://app.celoxis.com/psa/tasks/{t.Id}");
            t.Name = f.Hacker.Phrase();
            t.Description = f.Lorem.Sentence();
            t.Hierarchy = f.Lorem.Word();
            t.TopmostTask = t.Name;
            t.SN = f.Random.Number(1, 100).ToString();
            t.Wbs = f.Random.Replace("#.#.#");
            t.Color = f.Internet.Color();
            t.PlannedStart = f.Date.Soon(14);
            t.PlannedFinish = f.Date.Future(1);
            t.ActualStart = f.Random.Bool(0.6f) ? f.Date.Recent(7).ToString() : null;
            t.ActualFinish = f.Random.Bool(0.3f) ? f.Date.Recent(3) : null;
            t.ProjectedStart = f.Random.Bool(0.5f) ? f.Date.Soon(7) : null;
            t.ProjectedFinish = f.Random.Bool(0.5f) ? f.Date.Future(2) : null;
            t.PlannedEffort = f.Random.Number(1, 80).ToString();
            t.ScheduleType = f.PickRandom("FIXED_WORK", "FIXED_DURATION", "FIXED_UNITS");
            t.Duration = f.Random.Number(1, 10) + "d";
            t.Predecessors = "";
            t.Successors = "";
            t.ConstraintType = "ASAP";
            t.ConstraintDate = null;
            t.Priority = "NORMAL";
            t.ActualPercentComplete = f.Random.Number(0, 100).ToString();
            t.RemainingEffort = f.Random.Number(0, 40).ToString();
            t.Created = f.Date.Past(1);
            t.LastModified = f.Date.Recent(7);
            t.LastUpdate = f.Lorem.Sentence();
            t.AllUpdates = f.Lorem.Paragraph();
            t.LastUpdated = f.Date.Recent(3);
            t.Resources = f.Name.FullName();
            t.ScheduleHealth = f.PickRandom(FakerConstants.ScheduleHealths);
            t.BudgetHealth = f.PickRandom(FakerConstants.BudgetHealths);
            t.PlannedRevenue = f.Random.Decimal(0, 10000).ToString();
            t.PlannedLaborRevenue = (int.Parse(t.PlannedRevenue) * 0.8m).ToString();
            t.Budget = f.Random.Decimal(0, 8000).ToString();
            t.FixedPrice = f.Random.Decimal(0, 5000).ToString();
            t.BillingType = "FIXED_PRICE";
            t.PlannedCost = f.Random.Decimal(0, 6000).ToString();
            t.ActualCost = f.Random.Decimal(0, 5000).ToString();
            t.ActualLaborHours = f.Random.Number(0, 60).ToString();
            t.MyEstHours = f.Random.Number(0, 40).ToString();
            t.MyActHours = f.Random.Number(0, 35).ToString();
            t.Milestone = f.Random.Bool(0.1f) ? "Yes" : "No";
            t.IsManuallyScheduled = "No";
            t.IsTimeAllowed = "Yes";
            t.Critical = f.Random.Bool(0.2f) ? "Yes" : "No";
            t.ShowInTimeline = "No";
            t.ExternalKey = "";
            t.Project = new DataFieldWrapper<CeloxisProject>(f.Random.AlphaNumeric(8).ToUpper());
        });
}
