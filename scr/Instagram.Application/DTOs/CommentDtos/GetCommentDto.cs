namespace Instagram.Application.DTOs.CommentDtos;

public class GetCommentDto
{
    public long CommentId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long PostId { get; set; }
    public long UserId { get; set; }
}
