using Apollo.BusinessCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.BusinessCard.Infrastructure.Persistiance.DbSchema;

public class BusinessCardAttachmentSchema : IEntityTypeConfiguration<BusinessCardAttachment>
{
    public void Configure(EntityTypeBuilder<BusinessCardAttachment> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Created)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("getDate()");

        builder.Property(x => x.CreatedBy)
            .IsRequired(true);
    }
}
