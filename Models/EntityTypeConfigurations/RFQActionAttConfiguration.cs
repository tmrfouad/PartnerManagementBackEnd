using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class RFQActionAttConfiguration : IEntityTypeConfiguration<RFQActionAtt>
    {
        public void Configure(EntityTypeBuilder<RFQActionAtt> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasKey(a => new {a.RFQActionId, a.FileName});

            // Constraints & Datatypes
            // entityTypeBuilder.Property(r => r.FileName).IsRequired();
            // entityTypeBuilder.Property(r => r.FileUrl).IsRequired();

            // Relations

            // Not mapped properties
        }
    }
}