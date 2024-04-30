using infoManager.Models;
using Microsoft.EntityFrameworkCore;

namespace infoManager.Data
{
    public class InfoManagerDbContext : DbContext
    {
        public InfoManagerDbContext(DbContextOptions<InfoManagerDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("ServerConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder
                .Entity<Person>()
                .HasData(
                new Person
                {
                    Id = 1,
                    Name = "Alex", 
                    Cpf = "85252256063",
                    Birthday = new DateOnly(2000, 05, 13),
                    Status = Models.Enums.StatusEnum.Active
                },
                new Person
                {
                    Id = 2,
                    Name = "Bernardo",
                    Cpf = "42002341060",
                    Birthday = new DateOnly(1995, 01, 13),
                    Status = Models.Enums.StatusEnum.Suspended
                },
                new Person
                {
                    Id = 3,
                    Name = "Carlos",
                    Cpf = "42002341060",
                    Birthday = new DateOnly(1989, 01, 21),
                    Status = Models.Enums.StatusEnum.Pending
                },
                new Person
                {
                    Id = 4,
                    Name = "Denilda",
                    Cpf = "95534329050",
                    Birthday = new DateOnly(1978, 04, 21),
                    Status = Models.Enums.StatusEnum.Disabled
                }
                );

            modelBuilder
                .Entity<PhoneNumber>()
                .HasData(
                    new PhoneNumber
                    {
                        Id = 1,
                        PersonId = 1,
                        Number = "(47) 99965-1236",
                        Type = Models.Enums.PhoneType.CellPhone
                    },
                    new PhoneNumber
                    {
                        Id = 2,
                        PersonId = 1,
                        Number = "(47) 3393-1236",
                        Type = Models.Enums.PhoneType.Residential
                    },
                    new PhoneNumber
                    {
                        Id = 3,
                        PersonId = 1,
                        Number = "(47) 98965-1276",
                        Type = Models.Enums.PhoneType.Commercial
                    }
                );
        }
    }
}
