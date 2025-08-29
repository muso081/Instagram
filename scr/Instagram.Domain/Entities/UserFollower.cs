namespace Instagram.Domain.Entities;

public class UserFollower
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long FollowingUserId { get; set; }
    public User FollowingUser { get; set; }
    public DateTime FollowedAt { get; set; }
}
