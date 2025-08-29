namespace Instagram.Application.DTOs.UserFollowerDtos;

public class GetUserFollowerDto
{
    public long UserId { get; set; }
    public long FollowingUserId { get; set; }
    public DateTime FollowedAt { get; set; }
}
