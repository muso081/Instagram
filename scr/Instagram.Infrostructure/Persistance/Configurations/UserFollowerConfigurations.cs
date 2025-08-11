using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistance.Configurations;

public class UserFollowerConfigurations : IEntityTypeConfiguration<UserFollower>
{
    public void Configure(EntityTypeBuilder<UserFollower> builder)
    {
        builder.ToTable("UserFollowers");

        builder.HasKey(uf => new { uf.FollowerId, uf.FollowedId });

        builder.HasOne(uf => uf.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(uf => uf.FollowerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uf => uf.Followed)
            .WithMany(u => u.Followers)
            .HasForeignKey(uf => uf.FollowedId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
