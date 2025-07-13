using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateAppRequestFaker : Faker<CreateAppRequest>
{
    public CreateAppRequestFaker() =>
        Rules((f, r) =>
        {
            r.Name = f.Hacker.Phrase();
            r.Description = f.Lorem.Paragraph();
            r.App = f.PickRandom("Bug", "Issue", "Risk", "Change Request");
            r.State = "Reported";
            r.Priority = Priorities.Normal;
            r.Project = f.Random.AlphaNumeric(8).ToUpper();
            r.Requestor = f.Name.FullName();
            r.DueDate = f.Random.Bool(0.6f) ? f.Date.Future(1) : null;
            r.Assignee = f.Random.Bool(0.8f) ? f.Name.FullName() : null;
        });
}
