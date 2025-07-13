using Celoxis.Api.Models;
using Celoxis.Api.Tests.Fakers;

namespace Celoxis.Api.Tests.Helpers;

public static class TestDataGenerator
{
    public static CeloxisProjectFaker GetProjectFaker() => new();
    public static CeloxisTaskFaker GetTaskFaker() => new();
    public static CeloxisTimeEntryFaker GetTimeEntryFaker() => new();
    public static CeloxisUserFaker GetUserFaker() => new();
    public static CeloxisAppFaker GetAppFaker() => new();
    public static TaskUpdateFaker GetTaskUpdateFaker() => new();
    public static CeloxisExpenseFaker GetExpenseFaker() => new();
    public static CreateProjectRequestFaker GetCreateProjectRequestFaker() => new();
    public static CreateTaskRequestFaker GetCreateTaskRequestFaker() => new();
    public static CreateAppRequestFaker GetCreateAppRequestFaker() => new();
    public static CreateTaskUpdateRequestFaker GetCreateTaskUpdateRequestFaker() => new();
    public static CreateTimeEntryRequestFaker GetCreateTimeEntryRequestFaker() => new();
    public static CreateUserRequestFaker GetCreateUserRequestFaker() => new();
    public static UpdateAppRequestFaker GetUpdateAppRequestFaker() => new();
    public static UpdateExpenseRequestFaker GetUpdateExpenseRequestFaker() => new();
    public static UpdateProjectRequestFaker GetUpdateProjectRequestFaker() => new();
    public static UpdateTaskRequestFaker GetUpdateTaskRequestFaker() => new();
    public static UpdateTimeEntryRequestFaker GetUpdateTimeEntryRequestFaker() => new();
    public static UpdateUserRequestFaker GetUpdateUserRequestFaker() => new();

    public static ApiResponse<T> CreateApiResponse<T>(T data, int totalRecords = 0, int? nextPage = null) =>
        new() { Data = data, TotalRecords = totalRecords, NextPage = nextPage };

    public static SingleResponse<T> CreateSingleResponse<T>(T data) =>
        new() { Data = data };
}
