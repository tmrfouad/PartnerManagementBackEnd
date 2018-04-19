using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class EmailSenderConfiguration : IEntityTypeConfiguration<EmailSender>
    {
        public void Configure(EntityTypeBuilder<EmailSender> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(e => e.Email).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(e => e.Id).IsRequired();
            entityTypeBuilder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            entityTypeBuilder.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
            entityTypeBuilder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(e => e.UniversalIP)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
        }
    }
}