namespace Instagram.Domain.Entities;

public class Like
{
    public long PostId { get; set; }
    public Post Post { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
    public DateTime LikedAt { get; set; } = DateTime.Now;
}
