using HouseRent.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRent.Server
{
    public class FlatDbContext : DbContext
    {

        public FlatDbContext(DbContextOptions<FlatDbContext> options) : base(options) { }
        public DbSet<Tenant> Tenants { get; set; } = default!;
        public DbSet<Rent> Rents { get; set; } = default!;
        public DbSet<RentItem> RentItems { get; set; } = default!;
        public DbSet<Flat> Flats { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentItem>().HasKey(o => new { o.RentID, o.FlatID });
            modelBuilder.Entity<Tenant>().HasData(
            new Tenant { TenantID = 1, TenantName = "Manjurul", Address = "Mirpur", Email = "Manjurul12@gmail.com" },
            new Tenant { TenantID = 2, TenantName = "syed", Address = "Mirpur", Email = "Manjurul12@gmail.com" },
            new Tenant { TenantID = 3, TenantName = "syed", Address = "Mirpur", Email = "Manjurul12@gmail.com" }
        // Add more seed data here
        );
        }
    }
}
