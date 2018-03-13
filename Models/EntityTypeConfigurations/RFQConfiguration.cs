using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class RFQConfiguration : IEntityTypeConfiguration<RFQ>
    {
        public void Configure(EntityTypeBuilder<RFQ> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(r => r.RFQCode).IsUnique();
            entityTypeBuilder.HasIndex(r => r.ContactPersonEmail).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(r => r.RFQCode).IsRequired();
            entityTypeBuilder.Property(r => r.CompanyEnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonEnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonEmail).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonMobile).IsRequired();
            entityTypeBuilder.Property(r => r.TargetedProduct).IsRequired();
            entityTypeBuilder.Property(r => r.SelectedBundle).IsRequired();
            entityTypeBuilder.Property(r => r.Status).IsRequired();
            entityTypeBuilder.Property(r => r.SubmissionTime)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();

            // Not Mapped Properties
            entityTypeBuilder.Ignore(r => r.SendEmail);
        }
    }
}