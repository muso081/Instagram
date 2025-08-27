using Instagram.Application.DTOs.CommentDtos;
using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;

namespace Instagram.Application.Services.CommentServices;

public class CommentService(ICommentRepository _commentRepository) : ICommentService
{
    public async Task<long> CreateComment(CreateCommentDto comment)
    {
        return await _commentRepository.InsertAsync(MapToCommentEntity(comment));
    }

    public async Task Delete(long commentId)
    {
        _commentRepository.Delete(commentId);
        await _commentRepository.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(long commentId)
    {
        var exists = _commentRepository.ExistsAsync(commentId);
        return exists;
    }

    public async Task<GetCommentDto> GetByIdAsync(long commentId)
    {
        return MapToGetCommentDto(await _commentRepository.GetByIdAsync(commentId));
    }

    public async Task<ICollection<GetCommentDto>> GetByPostIdAsync(long postId)
    {
        var comments = await _commentRepository.GetByPostIdAsync(postId);
        return comments.Select(MapToGetCommentDto).ToList();
    }

    public async Task<ICollection<GetCommentDto>> GetByUserIdAsync(long userId)
    {
        var comments = await _commentRepository.GetByUserIdAsync(userId);
        return comments.Select(MapToGetCommentDto).ToList();
    }

    public async Task Update(UpdateCommentDto comment)
    {
        var commentEntity = await _commentRepository.GetByIdAsync(comment.CommentId);
        commentEntity.Content = comment.Content;
        _commentRepository.Update(commentEntity);
        await _commentRepository.SaveChangesAsync();
    }

    private Comment MapToCommentEntity(CreateCommentDto comment)
    {
        return new Comment()
        {
            Content = comment.Content,
            PostId = comment.PostId,
            UserId = comment.UserId,
            CreatedAt = DateTime.UtcNow
        };
    }

    private GetCommentDto MapToGetCommentDto(Comment comment)
    {
        return new GetCommentDto()
        {
            Content = comment.Content,
            PostId = comment.PostId,
            UserId = comment.UserId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
