using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class LikeRepository(AppDbContext _appDbContext) : ILikeRepository
{
    public async Task<bool> HasUserLikedAsync(long userId, long postId)
    {
        var res = await _appDbContext.Likes.AnyAsync(l => l.UserId == userId && l.PostId == postId);
        return res;
    }

    public async Task LikeAsync(long userId, long postId)
    {
        var like = new Like
        {
            LikedAt = DateTime.UtcNow,
            PostId = postId,
            UserId = userId
        };
        await _appDbContext.Likes.AddAsync(like);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public async Task UnlikeAsync(long userId, long postId)
    {
        var like = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);

        if (like == null)
        {
            throw new Exception($"Like not found for UserId {userId} and PostId {postId}");
        }
        _appDbContext.Likes.Remove(like);
    }
}
