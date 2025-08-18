using Instagram.Application.DTOs.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Instagram.Application.Services.Helper;

internal class TokenService(JwtSettings _jwtSettings) : ITokenService
{
    public string GenerateAccessToken(UserTokenDto userTokenDto)
    {
        var token = new Claim[]
        {
            new Claim("UserId", userTokenDto.UserId.ToString()),
            new Claim("Username", userTokenDto.Username.ToString()),
            new Claim("Email", userTokenDto.Email.ToString()),
            new Claim("Role", userTokenDto.Role.ToString())
        };
        var code = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!));
        var codecredentials = new SigningCredentials(code, SecurityAlgorithms.HmacSha256);
        var expiresAt = _jwtSettings.Lifetime;
        var newtoken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: token,
            expires: TimeHelper.GetDateTime().AddHours(expiresAt),
            signingCredentials: codecredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(newtoken);
    }

    public string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        var res = RandomNumberGenerator.Create();
        res.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidatorParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!))
        };
        var generator = new JwtSecurityTokenHandler();
        return generator.ValidateToken(token, tokenValidatorParameters, out _);
    }
}
