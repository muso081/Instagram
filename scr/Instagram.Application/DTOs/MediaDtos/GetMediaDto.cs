using Instagram.Domain.Entities;

namespace Instagram.Application.DTOs.MediaDtos;

public class GetMediaDto
{
    public long MediaId { get; set; }

    public long PostId { get; set; }

    public string Url { get; set; }

    public MediaTypeEnumDto MediaType { get; set; }

    public enum MediaTypeEnumDto
    {
        Image,
        Video
    }
}
