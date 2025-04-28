using Apollo.BusinessCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apollo.BusinessCard.Infrastructure.Persistiance.DbSchema;

public class GenderLookupSchema : IEntityTypeConfiguration<GenderLookup>
{
    public void Configure(EntityTypeBuilder<GenderLookup> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasData(
            new GenderLookup() { Id = 1, Name = "Male" , IsDeleted = false},
            new GenderLookup() { Id = 2, Name = "Female", IsDeleted = false }
            );
    }
}
