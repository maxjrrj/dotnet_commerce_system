using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeeAdminPolicy")]

    public static IResult Action(QueryAllUsersWithClaimName query, int page = 1, int rows = 2)

    {
        return Results.Ok(query.Execute(page, rows));
    }
}
