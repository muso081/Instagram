
using Instagram.Application.Interfaces;

namespace Instagram.Application.Services.LikeServices;

public class LikeService(ILikeRepository _likeRepository) : ILikeService
{
    public async Task<bool> HasUserLikedAsync(long userId, long postId)
    {
        return await _likeRepository.HasUserLikedAsync(userId, postId);
    }

    public async Task LikeAsync(long userId, long postAsync)
    {
        await _likeRepository.LikeAsync(userId, postAsync);
    }

    public async Task UnlikeAsync(long userId, long postId)
    {
        await _likeRepository.UnlikeAsync(userId, postId);
    }
}
