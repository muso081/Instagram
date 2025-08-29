using Instagram.Application.DTOs.UserFollowerDtos;
using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;

namespace Instagram.Application.Services.UserFollowerServices;

public class UserFollowerService(IUserFollowerRepository _userFollowerRepository) : IUserFollowerService
{
    public async Task FollowAsync(long followerId, long followingId)
    {
        await _userFollowerRepository.FollowAsync(followerId, followingId);
    }

    public async Task<ICollection<GetUserFollowerDto>> GetFollowersAsync(long userId)
    {
        var followers = await _userFollowerRepository.GetFollowingAsync(userId);
        var res = followers.Select(MapToGetUserFollowerDto).ToList();
        return res;
    }

    public async Task<ICollection<GetUserFollowerDto>> GetFollowingAsync(long userId)
    {
        var res = await _userFollowerRepository.GetFollowingAsync(userId);
        var finalRes = res.Select(MapToGetUserFollowerDto).ToList();
        return finalRes;
    }

    public Task<bool> IsFollowingAsync(long followerId, long followingId)
    {
        return _userFollowerRepository.IsFollowingAsync(followerId, followingId);
    }

    public async Task UnfollowAsync(long followerId, long followingId)
    {
        await _userFollowerRepository.UnfollowAsync(followerId, followingId);

    }
    private GetUserFollowerDto MapToGetUserFollowerDto(UserFollower getUserFollowerDto)
    {
        var newUser = new GetUserFollowerDto()
        {
            FollowedAt = getUserFollowerDto.FollowedAt,
            FollowingUserId = getUserFollowerDto.FollowingUserId,
            UserId = getUserFollowerDto.UserId
        };
        return newUser;
    }
}
