using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.BusinessCard.Infrastructure.Persistiance.DbSchema;

public class BusinessCardSchema : IEntityTypeConfiguration<Domain.Entities.BusinessCard>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.BusinessCard> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.DateOfBirth)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Phone)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.Created)
            .IsRequired()
            .HasColumnType("datetime")
            .HasDefaultValueSql("getDate()");

        builder.Property(x => x.CreatedBy)
            .IsRequired(true);

        builder.HasOne(x => x.Gender)
            .WithMany()
            .HasForeignKey(x => x.GenderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Attachement)
            .WithMany()
            .HasForeignKey(x => x.AttachmentId);
    }
}
