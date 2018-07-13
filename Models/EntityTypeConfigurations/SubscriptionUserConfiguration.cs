using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class SubscriptionUserConfiguration : IEntityTypeConfiguration<SubscriptionUser>
    {
        public void Configure(EntityTypeBuilder<SubscriptionUser> builder)
        {
            builder.HasIndex(u => new { u.SubscriptionId, u.Name }).IsUnique();

            builder.Property(u => u.SubscriptionId).IsRequired();
            builder.Property(u => u.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.UniversalIP).IsRequired().HasColumnType("nvarchar(20)");
            builder.Property(u => u.Created).IsRequired().HasColumnType("datetime");
            builder.Property(u => u.Modified).HasColumnType("datetime");

            builder.HasOne(u => u.Subscription)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}