using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class RepresentativeConfigration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> entityTypeBuilder)
        {
            // Constraints & Datatypes
            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.Property(x => x.Created).HasColumnType("datetime");
            entityTypeBuilder.Property(x => x.UniversalIP).IsRequired();
            entityTypeBuilder.Property(x => x.Address).IsRequired();

            // Relations
            entityTypeBuilder
                .HasMany<RFQAction>(rep => rep.RFQActions)
                .WithOne(a => a.Representative)
                .HasForeignKey(a => a.RepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
