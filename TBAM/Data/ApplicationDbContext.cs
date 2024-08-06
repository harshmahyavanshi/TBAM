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
        public DbSet<ProductMaster> ProductMaster { get; set; }
        public DbSet<Workcentres> Workcentres { get; set; }
        public DbSet<PurposesOfTesting> PurposesOfTesting { get; set; }
        public DbSet<TestBatch> TestBatch { get; set; }
        public DbSet<TestBatchItem> TestBatchItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User
            modelBuilder.Entity<User>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            //Role
            modelBuilder.Entity<Role>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            modelBuilder.Entity<Role>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            //Plants
            modelBuilder.Entity<Plants>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Plants>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            //ProductMaster
            modelBuilder.Entity<ProductMaster>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<ProductMaster>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            //Workcentres
            modelBuilder.Entity<Workcentres>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Workcentres>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);
            
            //PurposesOfTesting
            modelBuilder.Entity<PurposesOfTesting>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<PurposesOfTesting>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);
                    
            //TestBatch
            modelBuilder.Entity<TestBatch>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<TestBatch>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);


            //TestBatchItem
            modelBuilder.Entity<TestBatchItem>()
                .Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<TestBatchItem>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server=localhost; Database=TBAM1; User Id=postgres; password=root");
            //optionsBuilder.UseNpgsql($"Server=10.10.3.69; Database=eIFU; User Id=postgres; password=root");
            //optionsBuilder.UseNpgsql($"Server=localhost; Database=eIFU; User Id=postgres; password=sasasasa#123");
        }

    }
}
