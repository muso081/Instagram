using Instagram.Application.DTOs.MediaDtos;

namespace Instagram.Application.DTOs.PostDtos;

public class GetPostDto
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public string Caption { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<GetMediaDto> Media { get; set; }
    public int LikesCount { get; set; }
}
