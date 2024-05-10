using infoManagerAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace infoManagerAPI.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Username)
                .IsRequired(true);

            builder
                .Property(p => p.Password)
                .IsRequired(true);

            builder
                .Property(n => n.Email)
                .IsRequired(true);

            builder
                .HasIndex(n => n.Email)
                .IsUnique();

            builder
                .Property(n => n.Role)
                .HasConversion<int>()
                .IsRequired(true);

        }
    }
}
