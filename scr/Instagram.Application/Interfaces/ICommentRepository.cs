using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(long commentId);
    Task<ICollection<Comment>> GetByPostIdAsync(long postId);
    Task<ICollection<Comment>> GetByUserIdAsync(long userId);
    Task<long> InsertAsync(Comment comment);
    void Update(Comment comment);
    void Delete(long commentId);
    Task<bool> ExistsAsync(long commentId);
    Task<int> SaveChangesAsync();
}
