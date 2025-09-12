using Instagram.Application.Services.LikeServices;

namespace Instagram.Server.Endpoints;

public static class LikeEndpoints
{
    public static void RegisterLikeEndpoints(this WebApplication web)
    {
        web.MapPost("api/like/{postId}", async (HttpContext context, long postId, ILikeService likeService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();

            await likeService.LikeAsync(long.Parse(userId), postId);

        }).WithName("LikePost")
        .WithTags("Like")
        .Produces(StatusCodes.Status200OK);


        web.MapDelete("api/unlike/{postId}", async (HttpContext context, long postId, ILikeService likeService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            await likeService.UnlikeAsync(long.Parse(userId), postId);
        }).WithName("UnlikePost")
        .WithTags("Like")
        .Produces(StatusCodes.Status200OK);

        web.MapGet("api/hasliked/{postId}", async (HttpContext context, long postId, ILikeService likeService) =>
        {
            var userId = context.User.FindFirst("UserId")?.Value;
            if (userId == null) throw new UnauthorizedAccessException();
            var hasLiked = await likeService.HasUserLikedAsync(long.Parse(userId), postId); 
            return hasLiked;
        }).WithName("HasUserLikedPost")
        .WithTags("Like")
        .Produces<bool>(StatusCodes.Status200OK);
    }
}
