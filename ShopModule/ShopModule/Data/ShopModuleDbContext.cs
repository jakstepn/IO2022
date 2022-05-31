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

        public ShopModuleDbContext()
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<ShopEmployee> ShopEmployees { get; set; }
        public virtual DbSet<ShopManager> ShopManagers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
    }
}
