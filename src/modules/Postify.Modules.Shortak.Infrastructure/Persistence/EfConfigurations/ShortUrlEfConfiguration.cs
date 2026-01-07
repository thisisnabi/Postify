using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Shortak.Core.Entities;

namespace Postify.Modules.Shortak.Infrastructure.Persistence.EfConfigurations;

public class ShortUrlEfConfiguration : IEntityTypeConfiguration<ShortUrl>
{
    public void Configure(EntityTypeBuilder<ShortUrl> builder)
    {
        builder.ToTable("ShortUrls");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OriginalUrl)
               .IsRequired()
               .HasMaxLength(2048);

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.ExpiresAt)
               .IsRequired(false);

        builder.Property(x => x.ClickCount)
               .IsRequired()
               .HasDefaultValue(0);
    }
}
