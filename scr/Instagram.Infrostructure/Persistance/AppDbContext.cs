using Instagram.Domain.Entities;
using Instagram.Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Infrastructure.Persistance;

public class AppDbContext : DbContext
{
   public DbSet<User> Users { get; set; }
   public DbSet<Post> Posts { get; set; }
   public DbSet<Media> Media { get; set; }
   public DbSet<Like> Likes { get; set; }
   public DbSet<Comment> Comments { get; set; }
   public DbSet<UserFollower> UserFollowers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder model)
    {

        base.OnModelCreating(model);

        model.ApplyConfiguration(new CommentConfigurations());
        model.ApplyConfiguration(new UserConfigurations());
        model.ApplyConfiguration(new PostConfigurations());
        model.ApplyConfiguration(new StoryConfigurations());
        model.ApplyConfiguration(new MediaConfigurations());
        model.ApplyConfiguration(new LikeConfigurations());

    }
}
