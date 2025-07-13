using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateTimeEntryRequestFaker : Faker<UpdateTimeEntryRequest>
{
    public UpdateTimeEntryRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Task = f.Random.Number(10000, 99999).ToString();
            r.User = f.Name.FullName();
            r.BillRate = f.Random.Number(100, 300);
            r.IsBillable = f.Random.Bool(0.8f);
            r.Comments = f.Lorem.Sentence();
            r.CostRate = f.Random.Number(50, 150);
            r.IsCostable = true;
            r.Date = f.Date.Recent(30);
            r.Hours = Math.Round(f.Random.Decimal(0.5m, 8m), 1).ToString();
            r.State = f.PickRandom(States.TimeEntry.Saved, States.TimeEntry.PendingApproval, States.TimeEntry.Approved);
            r.TimeCode = f.PickRandom("Default", "Meeting", "Development", "Testing");
        });
}
