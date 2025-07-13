using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateTaskRequestFaker : Faker<CreateTaskRequest>
{
    public CreateTaskRequestFaker() =>
        Rules((f, r) =>
        {
            r.Project = f.Random.AlphaNumeric(8).ToUpper();
            r.Name = f.Hacker.Phrase();
            r.PlannedEffort = f.Random.Number(8, 40).ToString();
            r.Duration = f.Random.Number(1, 5) + "d";
            r.Resources = f.Name.FullName();
            r.PlannedStart = f.Date.Soon(14);
            r.Description = f.Lorem.Sentence();
            r.Priority = Priorities.Normal;
        });
}
