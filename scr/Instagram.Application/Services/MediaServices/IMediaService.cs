using Instagram.Application.DTOs.MediaDtos;
using Instagram.Domain.Entities;

namespace Instagram.Application.Services.MediaServices;

public interface IMediaService
{
    Task<GetMediaDto> GetByIdAsync(long mediaId);
    Task<ICollection<GetMediaDto>> GetByPostAsync(long postId);
    Task InsertAsync(CreateMediaDto media);
    Task Delete(long mediaId);
}