using static Instagram.Domain.Entities.Media;

namespace Instagram.Application.DTOs.MediaDtos;

public class CreateMediaDto
{
    public long PostId { get; set; }
    public string Url { get; set; }
    public MediaTypeDtoEnum MediaType { get; set; }
}
public enum MediaTypeDtoEnum
{
    Image,
    Video
}
