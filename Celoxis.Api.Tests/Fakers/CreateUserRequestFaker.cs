using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateUserRequestFaker : Faker<CreateUserRequest>
{
    public CreateUserRequestFaker() =>
        Rules((f, r) =>
        {
            r.Name = f.Name.FullName();
            r.Email = f.Internet.Email(r.Name);
            r.Login = f.Internet.UserName(r.Name);
            r.WorkCalendar = "Default";
            r.BillRate = f.Random.Number(100, 300);
            r.CostRate = f.Random.Number(50, 150);
            r.Roles = "Staff";
            r.ReportingManager = f.Random.Bool(0.8f) ? f.Name.FullName() : null;
            r.Phone = f.Phone.PhoneNumber();
            r.Admin = f.Random.Bool(0.1f);
            r.PrimaryJobRole = f.Name.JobTitle();
            r.Password = f.Internet.Password(8);
        });
}
