using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class EmailSenderConfiguration : IEntityTypeConfiguration<EmailSender>
    {
        public void Configure(EntityTypeBuilder<EmailSender> builder)
        {
            // Indecies
            builder.HasIndex(e => e.Email).IsUnique();

            // Constraints & Datatypes
            builder.Property(e => e.Id).IsRequired();
            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("nvarchar(50)");
            builder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("datetime");
            builder.Property(e => e.UniversalIP)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
        }
    }
}