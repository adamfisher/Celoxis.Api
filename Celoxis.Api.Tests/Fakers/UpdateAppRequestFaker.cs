using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateAppRequestFaker : Faker<UpdateAppRequest>
{
    public UpdateAppRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Name = f.Hacker.Phrase();
            r.Description = f.Lorem.Paragraph();
            r.State = f.PickRandom("Reported", "In Progress", "Resolved", "Closed");
            r.Priority = f.PickRandom(Priorities.VeryHigh, Priorities.High, Priorities.Normal, Priorities.Low, Priorities.VeryLow);
            r.Assignee = f.Random.Bool(0.8f) ? f.Name.FullName() : null;
            r.DueDate = f.Random.Bool(0.6f) ? f.Date.Future(1) : null;
            r.LastUpdate = f.Lorem.Sentence();
        });
}
