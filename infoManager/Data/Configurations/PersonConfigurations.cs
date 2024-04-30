using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using infoManager.Models;

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
                .IsRequired()
                .HasMaxLength(11) 
                .IsUnicode(false);

            builder
                .HasIndex(p => p.Cpf)
                .IsUnique();

            builder
                .Property(p => p.Birthday)
                .HasColumnType("date")
                .IsRequired();
        }
    }
}
