using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Infrastructure.Persistence.EfConfigurations;
internal class CorporateProfileEfConfiguration : IEntityTypeConfiguration<CorporateProfile>
{
    public void Configure(EntityTypeBuilder<CorporateProfile> builder)
    {
        builder.ToTable("CorporateProfiles");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.EconomicId)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(24);
    }
}
