using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Profile.Core.Entities;

namespace Postify.Modules.Profile.Infrastructure.Persistence.EfConfigurations;
internal class IndividualProfileEfConfiguration : IEntityTypeConfiguration<IndividualProfile>
{
    public void Configure(EntityTypeBuilder<IndividualProfile> builder)
    {
        builder.ToTable("IndividualProfiles");

        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(true);

        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(true);

        builder.Property(x => x.PhoneNumber)
               .IsRequired()
               .HasMaxLength(15)
               .IsUnicode(false);

        builder.Ignore(x => x.FullName);

        builder.HasMany(x => x.CorporateProfiles)
               .WithOne(x => x.Owner)
               .HasForeignKey(x => x.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
