using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class RFQActionConfiguration : IEntityTypeConfiguration<RFQAction>
    {
        public void Configure(EntityTypeBuilder<RFQAction> entityTypeBuilder)
        {
            entityTypeBuilder.HasIndex(a => a.ActionCode).IsUnique();

            entityTypeBuilder.Property(r => r.ActionCode).IsRequired();
            entityTypeBuilder.Property(r => r.ActionTime).IsRequired();
            entityTypeBuilder.Property(r => r.ActionType).IsRequired();
            entityTypeBuilder.Property(r => r.CompanyRepresentative).IsRequired();
            entityTypeBuilder.Property(r => r.Comments).IsRequired();
            entityTypeBuilder.Property(r => r.SubmissionTime).IsRequired();
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();

            entityTypeBuilder
                .HasOne(a => a.RFQ)
                .WithMany(r => r.RFQActions)
                .HasForeignKey(a => a.RFQId);
        }
    }
}