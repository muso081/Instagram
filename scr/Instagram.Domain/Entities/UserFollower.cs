namespace Instagram.Domain.Entities;

public class UserFollower
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long FollowedUserId { get; set; }
    public User FollowedUser { get; set; }
    public DateTime FollowedAt { get; set; }
}
