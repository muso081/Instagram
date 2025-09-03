using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Instagram.Application.DTOs.MediaDtos;
using Instagram.Application.DTOs.PostDtos;
using Instagram.Application.Interfaces;
using Instagram.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;

namespace Instagram.Application.Services.PostServices;

public class PostService : IPostService
{

    private readonly Cloudinary _cloudinary;
    private readonly IPostRepository _postRepository;
    public PostService(IConfiguration configuration, IPostRepository postRepository)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
        _postRepository = postRepository;
    }

    public async Task<long> CreateAsync(CreatePostDto post)
    {
        return await _postRepository.InsertAsync(MapToCreatePostDto(post));
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
                Url = UploadFileToCloudinary(m.File),
                MediaType = (Media.MediaTypeEnum)m.MediaType
            }).ToList()
        };
    }


    public string UploadFileToCloudinary(IFormFile file)
    {

        var isImage = IsImage(file);
        var isVideo = IsVideo(file);

        if(!isImage || !isVideo)
        {
            throw new Exception("Invalid file type. Only image and video files are allowed.");
        }

        using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "files"
        };

        var uploadResult = _cloudinary.Upload(uploadParams);

        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            throw new Exception("Cloudinary upload failed: " + uploadResult.Error?.Message);
        }

        return uploadResult.SecureUrl.ToString();
    }




    private bool IsImage(IFormFile file)
    {
        var allowedHeaders = new Dictionary<string, byte[]>
        {
            { "jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { "png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } },
            { "gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
            { "bmp", new byte[] { 0x42, 0x4D } },
            { "webp", new byte[] { 0x52, 0x49, 0x46, 0x46 } }
        };

        using var reader = new BinaryReader(file.OpenReadStream());
        var fileHeader = reader.ReadBytes(4);

        return allowedHeaders.Any(h => fileHeader.Take(h.Value.Length).SequenceEqual(h.Value));
    }


    private bool IsVideo(IFormFile file)
    {
        var allowedHeaders = new Dictionary<string, byte[]>
    {
        { "mp4", new byte[] { 0x00, 0x00, 0x00, 0x18 } },
        { "mp4_alt", new byte[] { 0x66, 0x74, 0x79, 0x70 } },
        { "avi", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
        { "mkv", new byte[] { 0x1A, 0x45, 0xDF, 0xA3 } },
        { "mov", new byte[] { 0x66, 0x74, 0x79, 0x71 } },
        { "wmv", new byte[] { 0x30, 0x26, 0xB2, 0x75 } },
        { "flv", new byte[] { 0x46, 0x4C, 0x56 } }, 
        { "webm", new byte[] { 0x1A, 0x45, 0xDF, 0xA3 } }
    };

        using var reader = new BinaryReader(file.OpenReadStream());
        var fileHeader = reader.ReadBytes(12);

        return allowedHeaders.Any(h => fileHeader.Take(h.Value.Length).SequenceEqual(h.Value));
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

    //private Post MapToCreatePostDto(CreatePostDto postDto)
    //{
    //    return new Post
    //    {
    //        Caption = postDto.Caption,
    //        CreatedAt = DateTime.UtcNow,
    //        UserId = postDto.UserId,
    //        Media = postDto.Media.Select(m => new Media
    //        {
    //            MediaType = (Media.MediaTypeEnum)m.MediaType
    //        }).ToList()
    //    };
    //}

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
