using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class RepresentativeConfigration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> entityTypeBuilder)
        {
            // Constraints & Datatypes
            entityTypeBuilder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            entityTypeBuilder.Property(x => x.Created)
                .HasColumnType("datetime");
            entityTypeBuilder.Property(x => x.UniversalIP)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
            entityTypeBuilder.Property(x => x.Address)
                .IsRequired()
                .HasColumnType("nvarchar(250)");
            entityTypeBuilder.Property(x => x.DateOfBirth)
                .HasColumnType("datetime");
            entityTypeBuilder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("nvarchar(150)");

            // Relations
            entityTypeBuilder
                .HasMany<RFQAction>(rep => rep.RFQActions)
                .WithOne(a => a.Representative)
                .HasForeignKey(a => a.RepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
