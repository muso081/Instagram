namespace Instagram.Application.Services.LikeServices
{
    public interface ILikeService
    {
        Task<bool> HasUserLikedAsync(long userId, long postId);
        Task LikeAsync(long userId, long postAsync);
        Task UnlikeAsync(long userId, long postId);
    }
}