using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class InvoiceActivityConfiguration : IEntityTypeConfiguration<InvoiceActivity>
    {
        public void Configure(EntityTypeBuilder<InvoiceActivity> builder)
        {
            builder.Property(i => i.InvoiceId).IsRequired();
            builder.Property(i => i.Date).IsRequired().HasColumnType("datetime");
            builder.Property(i => i.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(i => i.Created).IsRequired().HasColumnType("datetime");
            builder.Property(i => i.Modified).HasColumnType("datetime");

            builder.HasOne(i => i.Invoice)
                .WithMany(s => s.InvoiceActivities)
                .HasForeignKey(i => i.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}