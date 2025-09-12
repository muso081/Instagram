using Instagram.Application.Services.UserFollowerServices;

namespace Instagram.Server.Endpoints;

public static class UserFollowerEndpoints
{
    public static void RegisterUserFollowerEndpoints(this WebApplication web)
    {
        web.MapPost("api/follow/{followedId}", async (HttpContext context, long followedId, IUserFollowerService userFollowerService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            await userFollowerService.FollowAsync(long.Parse(userId), followedId);
        }).WithName("FollowUser")
        .WithTags("UserFollower")
        .Produces(StatusCodes.Status200OK);

        web.MapDelete("api/unfollow/{followedId}", async (HttpContext context, long followedId, IUserFollowerService userFollowerService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            await userFollowerService.UnfollowAsync(long.Parse(userId), followedId);
        }).WithName("UnfollowUser")
        .WithTags("UserFollower")
        .Produces(StatusCodes.Status200OK);

        web.MapGet("api/isfollowing/{followedId}", async (HttpContext context, long followedId, IUserFollowerService userFollowerService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            var isFollowing = await userFollowerService.IsFollowingAsync(long.Parse(userId), followedId);
            return isFollowing;
        }).WithName("IsFollowingUser")
        .WithTags("UserFollower")
        .Produces<bool>(StatusCodes.Status200OK);

        web.MapGet("api/followers/{userId}", async (HttpContext context, long userId, IUserFollowerService userFollowerService) =>
        {
            var currentUserId = context.User.FindFirst("UserId")?.Value;
            if (currentUserId == null) throw new UnauthorizedAccessException();
            var followers = await userFollowerService.GetFollowersAsync(userId);
            return followers;
        }).WithName("GetFollowers")
        .WithTags("UserFollower")
        .Produces(StatusCodes.Status200OK);

        web.MapGet("api/following/{userId}", async (HttpContext context, long userId, IUserFollowerService userFollowerService) =>
        {
            var currentUserId = context.User.FindFirst("UserId")?.Value;
            if (currentUserId == null) throw new UnauthorizedAccessException();
            var following = await userFollowerService.GetFollowingAsync(userId);
            return following;
        }).WithName("GetFollowing")
        .WithTags("UserFollower")
        .Produces(StatusCodes.Status200OK);
    }
}
    
