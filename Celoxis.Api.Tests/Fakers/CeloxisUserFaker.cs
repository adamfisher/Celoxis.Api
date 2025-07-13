using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CeloxisUserFaker : Faker<CeloxisUser>
{
    public CeloxisUserFaker() =>
        Rules((f, u) =>
        {
            u.Id = f.Random.Number(100000, 999999).ToString();
            u.Url = new Uri($"https://app.celoxis.com/psa/users/{u.Id}");
            u.Name = f.Name.FullName();
            u.Email = f.Internet.Email(u.Name);
            u.Phone = f.Phone.PhoneNumber();
            u.Username = f.Internet.UserName(u.Name);
            u.LastAccessed = f.Date.Recent();
            u.WorkCalendar = "Default";
            u.Admin = f.Random.Bool(0.1f) ? "Yes" : "No";
            u.ReportingManager = f.Random.Bool(0.8f) ? f.Name.FullName() : "";
            u.BillRate = f.Random.Number(100, 300).ToString();
            u.CostRate = f.Random.Number(50, 150).ToString();
            u.Roles = "Staff";
            u.PrimaryJobRole = f.Name.JobTitle();
        });
}
