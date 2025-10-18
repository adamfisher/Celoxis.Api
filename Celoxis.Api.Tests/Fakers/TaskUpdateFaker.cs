using System;
using Bogus;
using Celoxis.Api.Models;
using Celoxis.Api.Tests.Helpers;

namespace Celoxis.Api.Tests.Fakers;

public sealed class TaskUpdateFaker : Faker<CeloxisTaskUpdate>
{
    public TaskUpdateFaker() =>
        Rules((f, t) =>
        {
            t.Id = f.Random.Number(100000, 999999).ToString();
            t.Url = new Uri($"https://app.celoxis.com/psa/taskupdates/{t.Id}");
            t.Project = new Association<CeloxisProject>() { Data = TestDataGenerator.GetProjectFaker().Generate() };
            t.Task = new Association<CeloxisTask>() { Data = TestDataGenerator.GetTaskFaker().Generate() };
            t.TaskUpdateBy = f.Name.FullName();
            t.Date = f.Date.RecentOffset();
            t.Comments = f.Lorem.Sentence();
            t.PercentComplete = f.Random.Number(0, 100);
            t.ActualStart = f.Random.Bool() ? f.Date.PastOffset() : null;
            t.ActualFinish = f.Random.Bool() ? f.Date.PastOffset() : null;
            t.Associations = new CeloxisTaskUpdateAssociations
            {
                Task = new Uri($"https://app.celoxis.com/psa/tasks/{t.Task.Data.Id}"),
                Project = new Uri($"https://app.celoxis.com/psa/tasks/{t.Task.Data.Id}")
            };
        });
}
