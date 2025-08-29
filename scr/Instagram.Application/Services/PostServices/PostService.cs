using Instagram.Application.DTOs.MediaDtos;
using Instagram.Application.DTOs.PostDtos;
using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Instagram.Application.Services.PostServices;

internal class PostService(IPostRepository _postRepository) : IPostService
{
    public async Task<long> CreateAsync(CreatePostDto post)
    {

        return await _postRepository.InsertAsync(MapToCreatePostDto(post));
    }

    public async Task Delete(long postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
       _postRepository.Delete(post!);
        await _postRepository.SaveChangesAsync();
    }

    public async Task<GetPostDto?> GetByIdAsync(long postId)
    {
         var post = await _postRepository.GetByIdAsync(postId);
         return MapToGetPostDto(post!);
    }

    public async Task<ICollection<GetPostDto>> GetByUserAsync(long userId)
    {
        var posts = await _postRepository.GetByUserAsync(userId);
        return posts.Select(MapToGetPostDto).ToList();
    }

    public async Task Update(UpdatePostDto updatedPost)
    {
        var res = await _postRepository.GetByIdAsync(updatedPost.PostId);

        res.PostId = updatedPost.PostId;
        res.Caption = updatedPost.Caption;
        
        
        _postRepository.Update(res);
        await _postRepository.SaveChangesAsync();
    }

    private Post MapToCreatePostDto(CreatePostDto postDto)
    {
        return new Post
        {
            Caption = postDto.Caption,
            CreatedAt = DateTime.UtcNow,
            UserId = postDto.UserId,
            Media = postDto.Media.Select(m => new Media
            {
                Url = m.Url,
                MediaType = (Media.MediaTypeEnum)m.MediaType
            }).ToList()
        };
    }

    private GetPostDto MapToGetPostDto(Post post)
    {
        return new GetPostDto
        {
            PostId = post.PostId,
            Caption = post.Caption,
            CreatedAt = post.CreatedAt,
            UserId = post.UserId,
            Media = post.Media.Select(m => new GetMediaDto
            {
                MediaId = m.MediaId,
                Url = m.Url,
                MediaType = (GetMediaDto.MediaTypeEnumDto)m.MediaType
            }).ToList()
        };
    }
}
