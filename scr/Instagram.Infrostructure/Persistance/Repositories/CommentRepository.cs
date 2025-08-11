using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Instagram.Infrastructure.Persistance.Repositories;

public class CommentRepository(AppDbContext _appDbContext) : ICommentRepository
{
    public async Task InsertAsync(Comment comment)
    {
        await _appDbContext.Comments.AddAsync(comment);
    }

    public void Delete(long commentId)
    {
        var res = _appDbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
        if (res == null) throw new Exception($"Comment with Id {commentId} is not found");
        _appDbContext.Comments.Remove(res);
    }

    public async Task<bool> ExistsAsync(long commentId)
    {
        return await _appDbContext.Comments.AnyAsync(c => c.CommentId == commentId);
    }

    public async Task<Comment> GetByIdAsync(long commentId)
    {
        var res = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
        if (res == null) throw new Exception($"Comment with Id {commentId} is not found");
        return res;
    }

    public async Task<IEnumerable<Comment>> GetByPostIdAsync(long postId)
    {
        var res = await _appDbContext.Posts.Include(c => c.Comments).
            FirstOrDefaultAsync(p => p.PostId == postId)
            ?? throw new Exception ($"Post with Id {postId} is not found");
        return res.Comments;
    }

    public async Task<IEnumerable<Comment>> GetByUserIdAsync(long userId)
    {
        var res = await _appDbContext.Users.Include(u => u.Comments)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new Exception($"User with Id {userId} is not found");
        return res.Comments;
    }

    public void Update(Comment comment)
    {
        _appDbContext.Comments.Update(comment);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _appDbContext.SaveChangesAsync();
    }
}
