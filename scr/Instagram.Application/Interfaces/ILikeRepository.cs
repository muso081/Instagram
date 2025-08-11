namespace Instagram.Application.Interfaces;

public interface ILikeRepository
{
    Task<bool> HasUserLikedAsync(long userId, long postId);
    Task LikeAsync(long userId, long postAsync);
    Task UnlikeAsync(long userId, long postId);
    Task<int> SaveChangesAsync();

}
