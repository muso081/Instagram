using Instagram.Application.DTOs.UserFollowerDtos;

namespace Instagram.Application.Services.UserFollowerServices;

public interface IUserFollowerService
{
    Task<bool> IsFollowingAsync(long followerId, long followingId);
    Task FollowAsync(long followerId, long followingId);
    Task UnfollowAsync(long followerId, long followingId);
    Task<ICollection<GetUserFollowerDto>> GetFollowersAsync(long userId);
    Task<ICollection<GetUserFollowerDto>> GetFollowingAsync(long userId);
}