using CloudinaryDotNet;
using Instagram.Application.Interfaces;
using Instagram.Application.Services.CommentServices;
using Instagram.Application.Services.Helper;
using Instagram.Application.Services.LikeServices;
using Instagram.Application.Services.MediaServices;
using Instagram.Application.Services.PostServices;
using Instagram.Application.Services.UserFollowerServices;
using Instagram.Application.Services.UserServices;
using Instagram.Infrastructure.Persistance.Repositories;



namespace Instagram.Server.Configurations;

public static class DependancyInjectionConfiguration
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();

        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        builder.Services.AddScoped<ILikeService, LikeService>();
        builder.Services.AddScoped<ILikeRepository, LikeRepository>();

        builder.Services.AddScoped<IMediaService, MediaService>();
        builder.Services.AddScoped<IMediaRepository, MediaRepository>();

        builder.Services.AddScoped<IUserFollowerService, UserFollowerService>();
        builder.Services.AddScoped<IUserFollowerRepository, UserFollowerRepository>();

        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        builder.Services.AddSingleton<JwtSettings>(jwtSettings);

        builder.Services.AddSingleton<Cloudinary>();

    }
}
