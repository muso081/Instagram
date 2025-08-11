using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistance.Configurations
{
    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(p => p.PostId);

            builder.Property(p => p.Caption)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.HasMany(p => p.Comments)
                   .WithOne(u => u.Post)
                   .HasForeignKey(c => c.PostId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Media)
                  .WithOne(u => u.Post)
                  .HasForeignKey(c => c.PostId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(l => l.Likes)
                 .WithOne(u => u.Post)
                 .HasForeignKey(c => c.PostId)
                 .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
