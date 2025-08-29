using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface IMediaRepository
{
    Task<Media> GetByIdAsync(long mediaId);
    Task<ICollection<Media>> GetByPostAsync(long postId);
    Task InsertAsync(Media media);
    void Delete(Media media);
    Task<int> SaveChangesAsync();
}
