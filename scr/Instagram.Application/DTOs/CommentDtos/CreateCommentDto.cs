namespace Instagram.Application.DTOs.CommentDtos;

public class CreateCommentDto
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public string Content { get; set; }
}
