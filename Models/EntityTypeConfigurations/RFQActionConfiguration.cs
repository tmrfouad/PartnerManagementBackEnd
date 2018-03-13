using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class RFQActionConfiguration : IEntityTypeConfiguration<RFQAction>
    {
        public void Configure(EntityTypeBuilder<RFQAction> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(a => a.ActionCode).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(r => r.ActionCode).IsRequired();
            entityTypeBuilder.Property(r => r.ActionTime)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(r => r.ActionType).IsRequired();
            entityTypeBuilder.Property(r => r.RepresentativeId).IsRequired();
            entityTypeBuilder.Property(r => r.Comments).IsRequired();
            entityTypeBuilder.Property(r => r.SubmissionTime)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();

            // Relations
            entityTypeBuilder
                .HasOne(a => a.RFQ)
                .WithMany(r => r.RFQActions)
                .HasForeignKey(a => a.RFQId);
        }
    }
}