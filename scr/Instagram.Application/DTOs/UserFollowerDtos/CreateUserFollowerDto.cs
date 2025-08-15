namespace Instagram.Application.DTOs.UserFollowerDtos;

public class CreateUserFollowerDto
{
    public long UserId { get; set; }
    public long FollowedUserId { get; set; }
}
