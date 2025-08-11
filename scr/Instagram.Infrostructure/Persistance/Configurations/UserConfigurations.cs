using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistance.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.Username).HasMaxLength(50).IsRequired();
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Password).HasMaxLength(100).IsRequired();

            builder.Property(u => u.Email).HasMaxLength(100).IsRequired();

            builder.Property(u => u.Bio).HasMaxLength(250);

            builder.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            builder.HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder
                .HasMany(u => u.Following)
                .WithMany(u => u.Followers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFollow",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.ClientCascade));

        }
    }
}
