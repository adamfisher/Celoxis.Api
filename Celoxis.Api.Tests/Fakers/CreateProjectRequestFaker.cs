using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateProjectRequestFaker : Faker<CreateProjectRequest>
{
    public CreateProjectRequestFaker() =>
        Rules((f, r) =>
        {
            r.Name = f.Company.CompanyName() + " Project";
            r.Manager = f.Name.FullName();
            r.PlannedStart = f.Date.Soon(30);
            r.State = States.Project.Active;
            r.Type = f.PickRandom(FakerConstants.ProjectTypes);
            r.Workspace = f.PickRandom(FakerConstants.Workspaces);
            r.Deadline = f.Random.Bool(0.7f) ? f.Date.Future(6) : null;
            r.Description = f.Lorem.Paragraph();
            r.Budget = f.Random.Decimal(10000, 100000);
            r.BillingType = f.PickRandom(BillingTypes.FixedPrice, BillingTypes.TimeAndMaterial);
            r.Priority = Priorities.Normal;
            r.Risk = Priorities.Normal;
        });
}
