using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisAppFaker : Faker<CeloxisApp>
{
    public CeloxisAppFaker() =>
        Rules((f, a) =>
        {
            a.Id = f.Random.Number(10000, 99999).ToString();
            a.Url = $"https://app.celoxis.com/psa/apps/{a.Id}";
            a.Name = f.Hacker.Phrase();
            a.Description = f.Lorem.Paragraph();
            a.App = f.PickRandom("Bug", "Issue", "Risk", "Change Request");
            a.State = f.PickRandom("Reported", "In Progress", "Resolved", "Closed");
            a.Age = f.Random.Number(1, 365) + "d";
            a.Requestor = f.Name.FullName();
            a.DueDate = f.Random.Bool(0.6f) ? f.Date.Future(1) : null;
            a.Created = f.Date.Past(1);
            a.Timeout = null;
            a.Completed = f.Random.Bool(0.3f) ? f.Date.Recent(30) : null;
            a.Priority = f.PickRandom(Priorities.VeryHigh, Priorities.High, Priorities.Normal, Priorities.Low, Priorities.VeryLow);
            a.LastUpdate = f.Lorem.Sentence();
            a.LastUpdatedOn = f.Date.Recent(7);
            a.AllUpdates = f.Lorem.Paragraph();
            a.Delayed = f.Random.Bool(0.2f) ? "Yes" : "No";
            a.RequestorVisible = "No";
            a.Open = f.Random.Bool(0.7f) ? "Yes" : "No";
            a.StateManager = f.Name.FullName();
            a.ActualRevenue = f.Random.Decimal(0, 5000);
            a.ActualCost = f.Random.Decimal(0, 4000);
            a.ActualEffort = f.Random.Number(0, 40).ToString();
            a.Project = $"https://app.celoxis.com/psa/api/v2/apps/{a.Id}/project";
            a.Assignee = f.Random.Bool(0.8f) ? new { id = f.Random.Number(100000, 999999).ToString(), name = f.Name.FullName() } : null;
        });
}
