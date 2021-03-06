using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class ProductEditionConfiguration : IEntityTypeConfiguration<ProductEdition>
    {
        public void Configure(EntityTypeBuilder<ProductEdition> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasKey(r => new {r.ProductId, r.Id});
            entityTypeBuilder.HasIndex(r => r.Id).IsUnique();
            entityTypeBuilder.HasIndex(r => new {r.ProductId, r.EnglishName}).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(r => r.Id)
                .IsRequired()
                .UseSqlServerIdentityColumn();
            entityTypeBuilder.Property(r => r.EnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ArabicName).IsRequired();
            entityTypeBuilder.Property(r => r.Created)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();

            // Relations
            entityTypeBuilder
                .HasOne(a => a.Product)
                .WithMany(r => r.ProductEditions)
                .HasForeignKey(a => a.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}