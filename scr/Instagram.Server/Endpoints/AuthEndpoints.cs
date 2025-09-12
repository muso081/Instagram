using Instagram.Application.DTOs;
using Instagram.Application.DTOs.UserDtos;
using Instagram.Application.Services;
using Microsoft.AspNetCore.Identity.Data;
using System.Runtime.CompilerServices;

namespace Instagram.Server.Endpoints;

public static class AuthEndpoints
{
    public static void RegisterAuthEndpoints(this WebApplication web)
    {
        web.MapPost("api/auth/login", async (LoginDto request, IAuthentificationService authService) =>
        {
            var response = await authService.SignInAsync(request);
            return Results.Ok(response);
        }).WithName("Login")
        .WithTags("Auth")
        .Produces<LoginRequest>(StatusCodes.Status200OK);

        web.MapPost("api/auth/register", async (CreateUserDto request, IAuthentificationService authService) =>
        {
            var response = await authService.SignUpAsync(request);
            return Results.Ok(response);
        }).WithName("Register")
        .WithTags("Auth")
        .Produces<long>(StatusCodes.Status200OK);
    }
}
