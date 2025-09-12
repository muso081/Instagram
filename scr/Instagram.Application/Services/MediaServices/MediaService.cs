using Instagram.Application.DTOs.MediaDtos;
using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;

namespace Instagram.Application.Services.MediaServices;

public class MediaService(IMediaRepository _mediaRepository) : IMediaService
{
    public async Task Delete(long mediaId)
    {
        var media = await _mediaRepository.GetByIdAsync(mediaId);
        _mediaRepository.Delete(media);
        await _mediaRepository.SaveChangesAsync();
    }

    public async Task<GetMediaDto> GetByIdAsync(long mediaId)
    {
        var res = await _mediaRepository.GetByIdAsync(mediaId);
        return MapToGetMediaDto(res);
    }

    public async Task<ICollection<GetMediaDto>> GetByPostAsync(long postId)
    {
        var res = await _mediaRepository.GetByPostAsync(postId);
        return res.Select(MapToGetMediaDto).ToList();
    }

    public async Task InsertAsync(Media media)
    {
        var newMedia = new Media()
        {
            MediaType = (Media.MediaTypeEnum)media.MediaType,
            PostId = media.PostId,
            Url = media.Url
        };
        await _mediaRepository.InsertAsync(newMedia);
        await _mediaRepository.SaveChangesAsync();
    }

    private GetMediaDto MapToGetMediaDto(Media media)
    {
        var result = new GetMediaDto()
        {
            MediaId = media.MediaId,
            MediaType = (GetMediaDto.MediaTypeEnumDto)media.MediaType,
            PostId = media.PostId,
            Url = media.Url
        };
        return result;
    }
}
