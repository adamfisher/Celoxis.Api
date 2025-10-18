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
            t.ActualStart = f.Random.Bool(0.6f) ? f.Date.Recent(7) : null;
            t.ActualFinish = f.Random.Bool(0.3f) ? f.Date.Recent(3) : null;
            t.ProjectedStart = f.Random.Bool(0.5f) ? f.Date.Soon(7) : null;
            t.ProjectedFinish = f.Random.Bool(0.5f) ? f.Date.Future(2) : null;
            t.PlannedEffort = f.Random.Number(1, 80);
            t.ScheduleType = f.PickRandom("FIXED_WORK", "FIXED_DURATION", "FIXED_UNITS");
            t.Duration = f.Random.Number(1, 10) + "d";
            t.Predecessors = "";
            t.Successors = "";
            t.ConstraintType = "ASAP";
            t.ConstraintDate = null;
            t.Priority = "NORMAL";
            t.ActualPercentComplete = f.Random.Number(0, 100);
            t.RemainingEffort = f.Random.Number(0, 40);
            t.Created = f.Date.Past(1);
            t.LastModified = f.Date.Recent(7);
            t.LastUpdate = f.Lorem.Sentence();
            t.AllUpdates = f.Lorem.Paragraph();
            t.LastUpdated = f.Date.Recent(3);
            t.Resources = f.Name.FullName();
            t.ScheduleHealth = f.PickRandom(FakerConstants.ScheduleHealths);
            t.BudgetHealth = f.PickRandom(FakerConstants.BudgetHealths);
            t.PlannedRevenue = f.Random.Decimal(0, 10000);
            t.PlannedLaborRevenue = t.PlannedRevenue * 0.8m;
            t.Budget = f.Random.Decimal(0, 8000);
            t.FixedPrice = f.Random.Decimal(0, 5000);
            t.BillingType = "FIXED_PRICE";
            t.PlannedCost = f.Random.Decimal(0, 6000);
            t.ActualCost = f.Random.Decimal(0, 5000);
            t.ActualLaborHours = f.Random.Number(0, 60);
            t.MyEstHours = f.Random.Number(0, 40);
            t.MyActHours = f.Random.Number(0, 35);
            t.Milestone = f.Random.Bool(0.1f);
            t.IsManuallyScheduled = false;
            t.IsTimeAllowed = true;
            t.Critical = f.Random.Bool(0.2f);
            t.ShowInTimeline = false;
            t.ExternalKey = "";
            t.Project = new Association<CeloxisProject> { Url = $"https://app.celoxis.com/psa/projects/{f.Random.Number(800000, 999999)}" };
            t.Parent = new Association<CeloxisTask> { Url = $"https://app.celoxis.com/psa/tasks/{f.Random.Number(10000, 99999)}" };
            t.Assignments = new Association<object[]> { Data = [] };
        });
}
