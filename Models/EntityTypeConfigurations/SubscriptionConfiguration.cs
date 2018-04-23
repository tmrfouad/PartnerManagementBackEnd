using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.Property(s => s.ProductId).IsRequired();
            builder.Property(s => s.ProductEditionId).IsRequired();
            builder.Property(p => p.PartnerId).IsRequired();
            builder.Property(p => p.Date).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.Duration).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(p => p.Created).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.Modified).HasColumnType("datetime");

            builder.HasOne(s => s.Partner)
            .WithMany(p => p.Subscriptions)
            .HasForeignKey(s => s.PartnerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}