using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace acscustomersgatebackend.Models.EntityTypeConfigurations 
{
    public class RepresentativeConfigration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SubmissionTime).HasColumnType("datetime");
            builder.Property(x => x.UniversalIP).IsRequired();
            builder.Property(x => x.Address).IsRequired();
        }
    }
}
