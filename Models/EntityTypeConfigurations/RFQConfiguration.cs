using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations
{
    public class RFQConfiguration : IEntityTypeConfiguration<RFQ>
    {
        public void Configure(EntityTypeBuilder<RFQ> entityTypeBuilder)
        {
            entityTypeBuilder.Property(r => r.RFQCode).IsRequired().HasMaxLength(200);
            entityTypeBuilder.Property(r => r.CompanyEnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonEnglishName).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonEmail).IsRequired();
            entityTypeBuilder.Property(r => r.ContactPersonMobile).IsRequired();
            entityTypeBuilder.Property(r => r.TargetedProduct).IsRequired();
            entityTypeBuilder.Property(r => r.SelectedBundle).IsRequired();
            entityTypeBuilder.Property(r => r.Status).IsRequired();
            entityTypeBuilder.Property(r => r.SubmissionTime).IsRequired();
            entityTypeBuilder.Property(r => r.UniversalIP).IsRequired();
        }
    }
}