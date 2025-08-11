using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(long postId);
    Task<IEnumerable<Post>> GetByUserAsync(long userId);
    Task InsertAsync(Post post);
    void Update(Post post);
    void Delete(Post post);
    Task<int> SaveChangesAsync();
}
