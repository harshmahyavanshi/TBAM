using Microsoft.EntityFrameworkCore;
using TBAM.Models;

namespace TBAM.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Plants> Plants { get; set; }
        public DbSet<ProductCodes> ProductCodes { get; set; }
        public DbSet<Workcentres> Workcentres { get; set; }
        public DbSet<PurposesOfTesting> PurposesOfTesting { get; set; }
        public DbSet<TestBatch> TestBatch { get; set; }
        public DbSet<TestBatchItem> TestBatchItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server=localhost; Database=TBAM; User Id=postgres; password=root");
            //optionsBuilder.UseNpgsql($"Server=10.10.3.69; Database=eIFU; User Id=postgres; password=root");
            //optionsBuilder.UseNpgsql($"Server=localhost; Database=eIFU; User Id=postgres; password=sasasasa#123");
        }

    }
}
