using Microsoft.AspNetCore.Http;

namespace Instagram.Application.DTOs.MediaDtos;

public class CreateMediaDto
{
    public long PostId { get; set; }
    public IFormFile File { get; set; }
    public MediaTypeDtoEnum MediaType { get; set; }


    public enum MediaTypeDtoEnum
    {
        Image,
        Video
    }
}

