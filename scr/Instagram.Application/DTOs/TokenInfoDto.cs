namespace Instagram.Application.DTOs;

public class TokenInfoDto
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public int ExpiresAt { get; set; }
    public string? TokenType { get; set; }
}
