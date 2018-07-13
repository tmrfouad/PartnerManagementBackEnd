using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.Property(i => i.InvoiceId).IsRequired();
            builder.Property(i => i.Description).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(i => i.Amount).IsRequired();
            builder.Property(i => i.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(i => i.Created).IsRequired().HasColumnType("datetime");
            builder.Property(i => i.Modified).HasColumnType("datetime");

            builder.HasOne(i => i.Invoice)
                .WithMany(s => s.InvoiceDetails)
                .HasForeignKey(i => i.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}