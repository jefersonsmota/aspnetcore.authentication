using authentication.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace authentication.infrastructure.Configurations
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsUnique(true);

            builder.Property(x => x.FirstName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique(true);

            builder.HasMany(x => x.Phones).WithOne(x => x.User);
        }
    }
}
