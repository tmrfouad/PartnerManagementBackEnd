using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(p => p.Id).IsUnique();
            entityTypeBuilder.HasIndex(p => p.EnglishName).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(p => p.Id).IsRequired();
            entityTypeBuilder.Property(p => p.EnglishName).IsRequired();
            entityTypeBuilder.Property(p => p.ArabicName).IsRequired();
            entityTypeBuilder.Property(p => p.Created)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(p => p.UniversalIP).IsRequired();
        }
    }
}