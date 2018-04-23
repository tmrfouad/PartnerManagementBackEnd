using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class RFQConfiguration : IEntityTypeConfiguration<RFQ>
    {
        public void Configure(EntityTypeBuilder<RFQ> builder)
        {
            // Indecies
            builder.HasIndex(r => r.RFQCode).IsUnique();
            builder.HasIndex(r => r.ContactPersonEmail).IsUnique();

            // Constraints & Datatypes
            builder.Property(r => r.RFQCode).IsRequired();
            builder.Property(r => r.CompanyEnglishName).IsRequired();
            builder.Property(r => r.ContactPersonEnglishName).IsRequired();
            builder.Property(r => r.ContactPersonEmail).IsRequired();
            builder.Property(r => r.ContactPersonMobile).IsRequired();
            builder.Property(r => r.TargetedProductId).IsRequired();
            builder.Property(r => r.SelectedEditionId).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.SubmissionTime)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(r => r.UniversalIP).IsRequired();

            // Relations
            builder.HasOne(r => r.TargetedProduct)
                .WithMany(p => p.RFQs)
                .HasForeignKey(r => r.TargetedProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(r => r.SelectedEdition)
                .WithMany(e => e.RFQs)
                .HasForeignKey(r => new { r.TargetedProductId, r.SelectedEditionId })
                .OnDelete(DeleteBehavior.Restrict);
                

            // Not Mapped Properties
            builder.Ignore(r => r.SendEmail);
        }
    }
}