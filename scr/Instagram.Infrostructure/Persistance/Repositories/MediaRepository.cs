using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class MediaRepository(AppDbContext _appDbContext) : IMediaRepository
{
    public async Task InsertAsync(Media media)
    {
        await _appDbContext.Media.AddAsync(media);
    }

    public void Delete(Media media)
    {
        _appDbContext.Media.Remove(media);
    }

    public async Task<Media> GetByIdAsync(long mediaId)
    {
        var res = await _appDbContext.Media
            .FirstOrDefaultAsync(m => m.MediaId == mediaId);
        if(res == null) throw new Exception($"Media not found with Id {mediaId}");

        return res;
    }

    public async Task<IEnumerable<Media>> GetByPostAsync(long postId)
    {
        var res = await _appDbContext.Posts
            .Include(p => p.Media)
            .FirstOrDefaultAsync(m => m.PostId == postId);
        if (res == null) throw new Exception($"Post not found with Id {postId}");

        return res.Media;
    }

    public async Task<IEnumerable<Media>> GetByStoryAsync(long storyId)
    {
        var res = await _appDbContext.Media
            .Where(m => m.PostId == storyId)
            .ToListAsync();
        return res;
    }

    public async Task<int> SaveChangesAsync()
    {
       return await _appDbContext.SaveChangesAsync();
    }
}
