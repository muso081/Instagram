using Instagram.Application.DTOs.MediaDtos;

namespace Instagram.Application.DTOs.PostDtos;

public class CreatePostDto
{
    public long UserId { get; set; }
    public string Caption { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CreateMediaDto> Media { get; set; }
}
