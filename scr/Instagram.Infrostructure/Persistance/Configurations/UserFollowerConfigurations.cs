using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistance.Configurations;

public class UserFollowerConfigurations : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.ToTable("UserFollowers");

        builder.HasKey(uf => new { uf.UserId, uf.FollowingUser });

        builder.HasOne(uf => uf.User)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uf => uf.FollowingUser)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowingUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
