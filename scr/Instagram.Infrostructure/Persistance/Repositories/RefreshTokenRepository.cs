using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepository(AppDbContext _appDbContext) : IRefreshTokenRepository
{
    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
       await _appDbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task RemoveRefreshTokenAsync(string token)
    {
        var res = await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token);
        if (res == null)
        {
            throw new ArgumentException("Refresh token is not found to be deleted.");
        }

        _appDbContext.RefreshTokens.Remove(res);
    }

    public async Task<RefreshToken> SelectRefreshToken(string refreshToken, long userId)
    {
        var res = await _appDbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == refreshToken && x.UserId == userId);
        return res;
    }
}
