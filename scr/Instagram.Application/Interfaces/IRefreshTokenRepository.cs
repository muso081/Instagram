using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface IRefreshTokenRepository
{
    public Task AddRefreshTokenAsync(RefreshToken refreshToken);
    public Task RemoveRefreshTokenAsync(string token);
    public Task<RefreshToken> SelectRefreshToken(string refreshToken, long userId );
}
