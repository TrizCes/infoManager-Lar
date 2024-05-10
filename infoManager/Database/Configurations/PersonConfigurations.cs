using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using infoManagerAPI.Models;

namespace infoManagerAPI.Data.Configurations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

            builder
                .Property(p => p.Cpf)
                .IsRequired(true)
                .HasMaxLength(11) 
                .IsUnicode(false);

            builder
                .HasIndex(p => p.Cpf)
                .IsUnique(true);

            builder
                .Property(p => p.Birthday)
                .HasColumnType("date")
                .IsRequired(true);

            builder
                .Property(p => p.Status)
                .HasConversion<int>()
                .IsRequired(true);

        }
    }
}
