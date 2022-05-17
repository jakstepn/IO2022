using Complaints;
using Microsoft.EntityFrameworkCore;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Data
{
    public class ShopModuleDbContext : DbContext
    {
        public ShopModuleDbContext(DbContextOptions<ShopModuleDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShopEmployee> ShopEmployees { get; set; }
        public DbSet<ShopManager> ShopManagers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Seed();
        }
    }
}
