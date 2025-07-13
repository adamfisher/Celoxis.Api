using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class UpdateUserRequestFaker : Faker<UpdateUserRequest>
{
    public UpdateUserRequestFaker() =>
        Rules((f, r) =>
        {
            r.Id = f.Random.Number(100000, 999999).ToString();
            r.Name = f.Name.FullName();
            r.Email = f.Internet.Email(r.Name);
            r.Phone = f.Phone.PhoneNumber();
            r.BillRate = f.Random.Number(100, 300);
            r.CostRate = f.Random.Number(50, 150);
            r.Admin = f.Random.Bool(0.1f);
            r.PrimaryJobRole = f.Name.JobTitle();
            r.ReportingManager = f.Random.Bool(0.8f) ? f.Name.FullName() : null;
            r.WorkCalendar = "Default";
            r.Username = f.Internet.UserName(r.Name);
            r.Locale = "en-US";
            r.Password = f.Internet.Password(8);
        });
}
