using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateTaskRequestFaker : Faker<UpdateTaskRequest>
{
    public UpdateTaskRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Name = f.Hacker.Phrase();
            r.Description = f.Lorem.Sentence();
            r.PlannedEffort = f.Random.Number(8, 40);
            r.Duration = f.Random.Number(1, 5) + "d";
            r.Resources = f.Name.FullName();
            r.PlannedStart = f.Date.Soon(14);
            r.PlannedFinish = f.Date.Future(1);
            r.Priority = f.PickRandom(Priorities.VeryHigh, Priorities.High, Priorities.Normal, Priorities.Low, Priorities.VeryLow);
            r.ScheduleType = f.PickRandom("FIXED_WORK", "FIXED_DURATION", "FIXED_UNITS");
            r.ConstraintType = "ASAP";
            r.BillingType = "FIXED_PRICE";
            r.Budget = f.Random.Decimal(0, 8000);
            r.FixedPrice = f.Random.Decimal(0, 5000);
            r.ActualPercentComplete = f.Random.Number(0, 100);
            r.RemainingEffort = f.Random.Number(0, 40);
            r.Milestone = f.Random.Bool(0.1f);
            r.IsManuallyScheduled = false;
            r.IsTimeAllowed = true;
            r.ShowInTimeline = false;
            r.Color = f.Internet.Color();
        });
}
