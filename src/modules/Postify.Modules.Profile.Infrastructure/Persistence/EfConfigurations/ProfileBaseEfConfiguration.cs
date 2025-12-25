using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Infrastructure.Persistence.EfConfigurations;

internal class ProfileBaseEfConfiguration : IEntityTypeConfiguration<ProfileBase>
{
    public void Configure(EntityTypeBuilder<ProfileBase> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NationalId)
               .IsRequired()
               .HasMaxLength(11)
               .IsUnicode(false);
    }
}
