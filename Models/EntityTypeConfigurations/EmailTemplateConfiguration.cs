using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PartnerManagement.Models.EntityTypeConfigurations
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> entityTypeBuilder)
        {
            // Indecies
            entityTypeBuilder.HasIndex(e => e.Subject).IsUnique();

            // Constraints & Datatypes
            entityTypeBuilder.Property(e => e.Id).IsRequired();
            entityTypeBuilder.Property(e => e.Subject)
                .IsRequired()
                .HasColumnType("nvarchar(150)");
            entityTypeBuilder.Property(e => e.HtmlTemplate).IsRequired();
            entityTypeBuilder.Property(e => e.Created)
                .IsRequired()
                .HasColumnType("datetime");
            entityTypeBuilder.Property(e => e.UniversalIP)
                .IsRequired()
                .HasColumnType("nvarchar(20)");
        }
    }
}