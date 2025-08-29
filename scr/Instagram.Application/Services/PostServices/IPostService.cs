using Instagram.Application.DTOs.PostDtos;

namespace Instagram.Application.Services.PostServices;

public interface IPostService
{
    Task<GetPostDto?> GetByIdAsync(long postId);
    Task<ICollection<GetPostDto>> GetByUserAsync(long userId);
    Task<long> CreateAsync(CreatePostDto post);
    Task Update(UpdatePostDto post);
    Task Delete(long post);
}