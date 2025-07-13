using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisExpenseFaker : Faker<CeloxisExpense>
{
    public CeloxisExpenseFaker() =>
        Rules((f, e) =>
        {
            e.Id = f.Random.Number(100000, 999999).ToString();
            e.Url = $"https://app.celoxis.com/psa/expenses/{e.Id}";
            e.Name = f.Commerce.ProductName();
            e.Description = f.Lorem.Sentence();
            e.Date = f.Date.Recent(30);
            e.Amount = f.Random.Decimal(10, 1000);
            e.Category = f.PickRandom("Travel", "Meals", "Equipment", "Software", "Other");
            e.Project = $"https://app.celoxis.com/psa/api/v2/expenses/{e.Id}/project";
            e.Task = $"https://app.celoxis.com/psa/api/v2/expenses/{e.Id}/task";
            e.User = $"https://app.celoxis.com/psa/api/v2/expenses/{e.Id}/user";
            e.State = f.PickRandom("Draft", "Submitted", "Approved", "Rejected");
            e.Created = f.Date.Recent(7);
            e.LastModified = e.Created;
        });
}
