using Instagram.Application.DTOs.CommentDtos;

namespace Instagram.Application.Services.CommentServices;

public interface ICommentService
{
    Task<GetCommentDto> GetByIdAsync(long commentId);
    Task<ICollection<GetCommentDto>> GetByPostIdAsync(long postId);
    Task<ICollection<GetCommentDto>> GetByUserIdAsync(long userId);
    Task<long> CreateComment(CreateCommentDto comment);
    Task Update(UpdateCommentDto comment);
    Task Delete(long commentId);
    Task<bool> ExistsAsync(long commentId);
}