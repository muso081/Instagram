using Instagram.Domain.Entities; // Replace with the actual namespace where Comment is defined

namespace Instagram.Application.Interfaces;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(long commentId);
    Task<IEnumerable<Comment>> GetByPostIdAsync(long postId);
    Task<IEnumerable<Comment>> GetByUserIdAsync(long userId);
    Task InsertAsync(Comment comment);
    void Update(Comment comment);
    void Delete(long commentId);
    Task<bool> ExistsAsync(long commentId);
    Task<int> SaveChangesAsync();
}
