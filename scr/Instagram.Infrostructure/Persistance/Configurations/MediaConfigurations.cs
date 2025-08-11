using Instagram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagram.Infrastructure.Persistance.Configurations
{
    public class MediaConfigurations : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Media");
            builder.HasKey(m => m.MediaId);

            builder.Property(m => m.MediaType)
                .IsRequired();
                

        }
    }
}
