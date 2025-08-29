using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface IUserFollowerRepository
{
    Task<bool> IsFollowingAsync(long followerId, long followingId);
    Task FollowAsync(long followerId, long followingId);
    Task UnfollowAsync(long followerId, long followingId);
    Task<ICollection<UserFollower>> GetFollowersAsync(long userId);
    Task<ICollection<UserFollower>> GetFollowingAsync(long userId);
    Task<int> SaveChangesAsync();
}