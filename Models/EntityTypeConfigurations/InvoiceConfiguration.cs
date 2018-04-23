using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerManagement.Models.Enumerations;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasIndex(i => i.SubscriptionId).IsUnique();
            builder.HasIndex(i => i.InvoiceNo).IsUnique();

            builder.Property(i => i.SubscriptionId).IsRequired();
            builder.Property(i => i.InvoiceNo).IsRequired().HasColumnType("nvarchar(25)");
            builder.Property(i => i.Date).IsRequired().HasColumnType("datetime");
            builder.Property(i => i.Price).IsRequired().HasDefaultValue(0);
            builder.Property(i => i.Status).IsRequired().HasDefaultValue(InvoiceStatus.Issued);
            builder.Property(i => i.Paid).IsRequired().HasDefaultValue(false);
            builder.Property(i => i.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(i => i.Created).IsRequired().HasColumnType("datetime");
            builder.Property(i => i.Modified).HasColumnType("datetime");

            builder.HasOne(i => i.Subscription)
                .WithMany(s => s.Invoices)
                .HasForeignKey(i => i.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}