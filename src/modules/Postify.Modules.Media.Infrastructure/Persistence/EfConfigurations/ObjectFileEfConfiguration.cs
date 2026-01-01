using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postify.Modules.Media.Core.Entities;

namespace Postify.Modules.Media.Infrastructure.Persistence.EfConfigurations;
public class ObjectFileEfConfiguration : IEntityTypeConfiguration<ObjectFile>
{
    public void Configure(EntityTypeBuilder<ObjectFile> builder)
    {
        builder.ToTable("ObjectFiles");

        builder.HasKey(of => of.Id);

        builder.Property(of => of.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(of => of.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(of => of.SizeInBytes)
            .IsRequired();

        builder.Property(of => of.CreatedAt)
            .IsRequired();

        builder.Property(of => of.StorageObjectId)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(of => of.BucketName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(of => of.AccessLevel)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(of => of.Status)
            .HasConversion<string>()    
            .IsRequired();
    }
}
