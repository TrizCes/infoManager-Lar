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

    }
}
