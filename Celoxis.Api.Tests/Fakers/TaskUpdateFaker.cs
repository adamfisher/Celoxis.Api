using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class TaskUpdateFaker : Faker<TaskUpdate>
{
    public TaskUpdateFaker() =>
        Rules((f, t) =>
        {
            t.Id = f.Random.Number(100000, 999999).ToString();
            t.Url = $"https://app.celoxis.com/psa/taskupdates/{t.Id}";
            t.Task = $"https://app.celoxis.com/psa/api/v2/taskupdates/{t.Id}/task";
            t.Update = f.Lorem.Paragraph();
            t.Created = f.Date.Recent(7);
            t.CreatedBy = f.Name.FullName();
        });
}
