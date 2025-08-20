namespace Instagram.Domain.Entities;

public class RefreshToken
{
    public long RefreshTokenId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
