using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateExpenseRequestFaker : Faker<UpdateExpenseRequest>
{
    public UpdateExpenseRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Name = f.Commerce.ProductName();
            r.Description = f.Lorem.Sentence();
            r.Date = f.Date.Recent(30);
            r.Amount = f.Random.Decimal(10, 1000);
            r.Category = f.PickRandom("Travel", "Meals", "Equipment", "Software", "Other");
            r.Project = f.Random.AlphaNumeric(8).ToUpper();
            r.Task = f.Random.Number(10000, 99999).ToString();
            r.User = f.Name.FullName();
            r.State = f.PickRandom("Draft", "Submitted", "Approved", "Rejected");
        });
}
