using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.Email).IsUnique();

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(p => p.Email).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(p => p.Created).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.Modified).HasColumnType("datetime");
        }
    }
}