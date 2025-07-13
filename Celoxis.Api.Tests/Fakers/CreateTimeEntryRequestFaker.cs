using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateTimeEntryRequestFaker : Faker<CreateTimeEntryRequest>
{
    public CreateTimeEntryRequestFaker() =>
        Rules((f, r) =>
        {
            r.User = f.Name.FullName();
            r.WorkItem = f.Random.Number(10000, 99999).ToString();
            r.Date = f.Date.Recent(30);
            r.Hours = Math.Round(f.Random.Decimal(0.5m, 8m), 1);
            r.TimeCode = f.PickRandom("Default", "Meeting", "Development", "Testing");
            r.Comments = f.Lorem.Sentence();
            r.State = States.TimeEntry.Saved;
        });
}
