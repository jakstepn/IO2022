using Microsoft.EntityFrameworkCore;
using ShopModule.Employees;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Data
{
    public class ShopModuleDbContext: DbContext
    {
        public ShopModuleDbContext(DbContextOptions<ShopModuleDbContext> options): base(options)
        {
            
        }

        public DbSet<ShopEmployee> ShopEmployees { get; set; }
        public DbSet<ShopManager> ShopManager { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Courier> Courier { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
