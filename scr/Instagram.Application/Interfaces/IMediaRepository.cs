using Instagram.Domain.Entities;

namespace Instagram.Application.Interfaces;

public interface IMediaRepository
{
    Task<Media> GetByIdAsync(long mediaId);
    Task<IEnumerable<Media>> GetByPostAsync(long postId);
    Task<IEnumerable<Media>> GetByStoryAsync(long storyId);
    Task InsertAsync(Media media);
    void Delete(Media media);
    Task<int> SaveChangesAsync();
}
