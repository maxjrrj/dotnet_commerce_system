﻿using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    
    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context, HttpContext http)
    {

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var category = new Category(categoryRequest.Name, userId, userId);  

        if (!category.IsValid)
        {   
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
        }

        context.Category.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id);

    }
}
