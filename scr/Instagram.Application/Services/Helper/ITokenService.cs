using Instagram.Application.DTOs.UserDtos;
using System.Security.Claims;

namespace Instagram.Application.Services.Helper;

public interface ITokenService
{
    public string GenerateAccessToken(UserTokenDto userTokenDto);
    public string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}