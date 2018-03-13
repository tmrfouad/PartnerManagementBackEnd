using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(r => r.Id).IsUnique();
            entityTypeBuilder.HasIndex(r => r.EnglishName).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(r => r.Id).IsRequired();
            entityTypeBuilder.Property(r => r.EnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ArabicName).IsRequired();
            entityTypeBuilder.Property(r => r.Created)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();
        }
    }
}