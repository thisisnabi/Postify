using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Notify.Core.Entities;

namespace Postify.Modules.Notify.Infrastructure.Persistence.EfConfigurations;
public class SmsNotificationEventEfConfiguration : IEntityTypeConfiguration<SmsNotificationEvent>
{
    public void Configure(EntityTypeBuilder<SmsNotificationEvent> builder)
    {
        builder.ToTable("SmsNotificationEvents");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.TrackingCode)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.EventType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(e => e.RemoteId)
            .HasMaxLength(100);

        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
