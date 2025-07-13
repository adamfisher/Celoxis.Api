using System;
using Bogus;
using Celoxis.Api.Models;

namespace Celoxis.Api.Tests.Fakers;

public sealed class CreateTaskUpdateRequestFaker : Faker<CreateTaskUpdateRequest>
{
    public CreateTaskUpdateRequestFaker() =>
        Rules((f, r) =>
        {
            r.Task = f.Random.Number(10000, 99999).ToString();
            r.Update = f.Lorem.Paragraph();
            r.CreatedBy = f.Name.FullName();
        });
}
