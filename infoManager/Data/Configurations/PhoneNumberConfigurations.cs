using infoManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace infoManagerAPI.Data.Configurations
{
    public class PhoneNumberConfigurations : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> builder)
        {
            builder.ToTable("PhoneNumbers");
            
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.PersonId)
                .IsRequired(true);

            builder
                .Property(p => p.Number)
                .IsRequired(true);

            builder
                .Property(p => p.Type)
                .HasConversion<int>()
                .IsRequired(true);

            builder
                .HasOne(n => n.Person)
                .WithMany(p => p.PhoneNumbers)
                .HasForeignKey(n => n.PersonId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
