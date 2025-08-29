using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _appDbContext;

    public PostRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Delete(Post post)
    {
        _appDbContext.Posts.Remove(post);
    }

    public async Task<Post?> GetByIdAsync(long postId)
    {
        var post = await _appDbContext.Posts.Include(x => x.Media).
            FirstOrDefaultAsync(u => u.PostId == postId);
        if (post == null)
        {
            return null;
        }
        return post;
    }

    public async Task<ICollection<Post>> GetByUserAsync(long userId)
    {
        var res = await _appDbContext.Posts.Include(m => m.Media)
            .Where(p => p.UserId == userId)
            .ToListAsync();
        return res;
    }

    public async Task<long> InsertAsync(Post post)
    {
        await _appDbContext.Posts.AddAsync(post);
        return post.PostId;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }

    public void Update(Post post)
    {
        _appDbContext.Posts.Update(post);
    }
}
