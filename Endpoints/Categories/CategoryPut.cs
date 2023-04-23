using Flunt.Notifications;
using IWantApp.Domain.Products;
using IWantApp.Endpoints.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;
    
    public static IResult Action([FromRoute] Guid id,HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {


        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = context.Category.Where(c => c.Id == id).FirstOrDefault();

        if (!category.IsValid) {
            return Results.NotFound(new { code = "404", message = "Not Found" });
        }

        category.EditInfo(categoryRequest.Name, categoryRequest.Active, userId);


        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());

        context.SaveChanges();

        return Results.Ok();

    }
}
