using Instagram.Application.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using static Instagram.Application.DTOs.UserDtos.CreateUserDto;
namespace Instagram.Server.Endpoints;

public static class UserEndpoints
{
    public static void RegisterUserEndpoints(this WebApplication web)
    {
        web.MapGet("api/users/{username}", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContent context, string username, IUserService user) =>
        {
            var userDto = await user.GetUserByUsername(username);
            return userDto;
        }).WithName("GetUserByUsername")
        .WithTags("User")
        .Produces(StatusCodes.Status200OK);




        web.MapGet("api/users/id/{id}", async (HttpContext context, long id, IUserService user) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            var userDto = await user.GetUserById(id);
            return userDto;
        }).WithName("GetUserById")
          .WithTags("User")
          .Produces(StatusCodes.Status200OK);




        web.MapGet("api/users/get-users", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContext context, IUserService user) =>
       {
           var userId = context.User.FindFirst("UserId")?.Value;
           if (userId == null) throw new UnauthorizedAccessException();
           var users = await user.GetAllUsers();
           return users;
       }).WithName("GetAllUsers")
          .WithTags("User")
          .Produces(StatusCodes.Status200OK);




        web.MapGet("api/users/email/{email}", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContext context, string email, IUserService user) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            var userDto = await user.GetUserByEmail(email);
            return userDto;
        }).WithName("GetUserByEmail")
          .WithTags("User")
          .Produces(StatusCodes.Status200OK);



        web.MapDelete("api/users/delete-user/{id}",[Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContext context, long userId, IUserService service) =>
        {
            var adminId = context.User.FindFirst("UserId")?.Value;
            if (adminId == null) throw new UnauthorizedAccessException();
            await service.DeleteUser(userId);
            return adminId;
        }).WithName("DeleteUser")
          .WithTags("User")
          .Produces(StatusCodes.Status200OK);


        web.MapPut("api/users/update-role/{userId}", [Authorize(Roles = "Admin, SuperAdmin")]
        async (HttpContext context, long userId, UserRoleDto role, IUserService service) =>
        {
            var adminId = context.User.FindFirst("UserId")?.Value;
            if (adminId == null) throw new UnauthorizedAccessException();
            await service.UpdateUserRole(userId, role);
            return adminId;
        }).WithName("UpdateUserRole")
          .WithTags("User")
          .Produces(StatusCodes.Status200OK);
    }
}
