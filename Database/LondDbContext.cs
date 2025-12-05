using LOND.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LOND.API.Database
{
    public class LondDbContext : DbContext
    {
        public LondDbContext()
        {

        }
        public LondDbContext(DbContextOptions<LondDbContext> options) : base(options)
        {
        }
        // Define your DbSets here
        public DbSet<LondUser> Lond_Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LondUser>().ToTable("LOND_Users");
            modelBuilder.Entity<LondUser>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
        }

    }
}
