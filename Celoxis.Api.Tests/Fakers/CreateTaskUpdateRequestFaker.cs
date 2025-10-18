using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateTaskUpdateRequestFaker : Faker<CreateTaskUpdateRequest>
{
    public CreateTaskUpdateRequestFaker() =>
        Rules((f, r) =>
        {
            r.Task = f.Random.Number(10000, 99999).ToString();
            r.Date = f.Date.Soon(14);
            r.Comments = f.Lorem.Sentence();
            r.PercentComplete = f.Random.Number(0, 100);
            r.ActualStart = f.Random.Bool(0.6f) ? f.Date.Recent(7) : null;
            r.ActualFinish = f.Random.Bool(0.3f) ? f.Date.Recent(3) : null;
        });
}
