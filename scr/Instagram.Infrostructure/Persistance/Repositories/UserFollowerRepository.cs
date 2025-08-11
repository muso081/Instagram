using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class UserFollowerRepository(AppDbContext _appDbContext) : IUserFollowerRepository
{
    public async Task FollowAsync(long userId, long followedUserId)
    {
        var follow = new UserFollower
        {  
            UserId = userId,
            FollowedUserId = followedUserId,
            FollowedAt = DateTime.UtcNow
        };
        await _appDbContext.UserFollowers.AddAsync(follow);
    }

    public async Task<IEnumerable<UserFollower>> GetFollowersAsync(long userId)
    {
        return await _appDbContext.UserFollowers.Where(u => u.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<UserFollower>> GetFollowingAsync(long userId)
    {
        return await _appDbContext.UserFollowers.Where(u => u.FollowedUserId == userId).ToListAsync();
    }

    public async Task<bool> IsFollowingAsync(long  userId, long followedUserId)
    {
        return await _appDbContext.UserFollowers.AnyAsync(u => u.UserId == userId && u.FollowedUserId == followedUserId);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public async Task UnfollowAsync(long userId, long followedUserId)
    {
        var res = await _appDbContext.UserFollowers.FirstOrDefaultAsync(u => u.UserId == userId && u.FollowedUserId == followedUserId);
        if (res == null) throw new Exception($"Follower with id {userId} is not found to delete");
        _appDbContext.UserFollowers.Remove(res);
    }
}
