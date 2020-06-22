using authentication.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.infrastructure.Configurations
{
    public class PhoneMap : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Number)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.AreaCode)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(x => x.CountryCode)
                .HasMaxLength(7)
                .IsRequired();

            builder.Ignore(x => x.Validation);
        }
    }
}
