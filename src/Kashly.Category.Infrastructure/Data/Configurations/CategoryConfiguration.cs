using Kashly.Category.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kashly.Category.Infrastructure.Data.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Category>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(Domain.Entities.Category.MaxDescriptionLength);

        builder.Property(c => c.Type)
            .HasConversion(new EnumToStringConverter<CategoryType>())
            .IsRequired();

        builder.Property(c => c.Icon)
            .IsRequired()
            .HasMaxLength(Domain.Entities.Category.MaxIconLength);

        builder.Property(c => c.Color)
            .IsRequired()
            .HasMaxLength(Domain.Entities.Category.MaxColorLength);

        builder.Property(c => c.UserId)
            .IsRequired()
            .HasMaxLength(Domain.Entities.Category.MaxUserIdLength);

        builder.HasIndex(c => new { c.Description, c.Type, c.UserId })
            .IsUnique()
            .HasDatabaseName("IX_Categories_Description_Type_UserId");

        builder.HasIndex(c => new { c.Id, c.UserId })
            .HasDatabaseName("IX_Categories_Id_UserId");
    }
}
