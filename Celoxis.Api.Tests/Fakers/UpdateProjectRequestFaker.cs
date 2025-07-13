using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateProjectRequestFaker : Faker<UpdateProjectRequest>
{
    public UpdateProjectRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Name = f.Company.CompanyName() + " Project";
            r.Manager = f.Name.FullName();
            r.PlannedStart = f.Date.Soon(30);
            r.Deadline = f.Random.Bool(0.7f) ? f.Date.Future(6) : null;
            r.Description = f.Lorem.Paragraph();
            r.Budget = f.Random.Decimal(10000, 100000);
            r.BillingType = f.PickRandom(BillingTypes.FixedPrice, BillingTypes.TimeAndMaterial);
            r.FixedPrice = f.Random.Decimal(0, 50000);
            r.Priority = f.PickRandom(Priorities.VeryHigh, Priorities.High, Priorities.Normal, Priorities.Low, Priorities.VeryLow);
            r.Risk = f.PickRandom(Priorities.VeryHigh, Priorities.High, Priorities.Normal, Priorities.Low, Priorities.VeryLow);
            r.Alignment = f.Random.Number(0, 100);
            r.Benefit = f.Random.Number(0, 100);
            r.Code = f.Random.AlphaNumeric(8).ToUpper();
            r.State = f.PickRandom(States.Project.Draft, States.Project.Active, States.Project.OnHold, States.Project.Completed);
            r.Type = f.PickRandom(FakerConstants.ProjectTypes);
            r.Workspace = f.PickRandom(FakerConstants.Workspaces);
        });
}
