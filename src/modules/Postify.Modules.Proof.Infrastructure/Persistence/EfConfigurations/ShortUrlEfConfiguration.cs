using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Proof.Core.Entities;

namespace Postify.Modules.Proof.Infrastructure.Persistence.EfConfigurations;

public class MessageProofEfConfiguration : IEntityTypeConfiguration<MessageProof>
{
    public void Configure(EntityTypeBuilder<MessageProof> builder)
    {
        builder.ToTable("MessageProofs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProfileId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.FileNames)
            .HasConversion(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.SeenCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.ProofType)
            .HasMaxLength(50)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
    }
}
