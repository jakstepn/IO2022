using Complaints;
using Microsoft.EntityFrameworkCore;
using ShopModule.Complaints;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Products;
using System;

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
            // Add seed addreses
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = System.Guid.Parse("EEEEEEEE-DDDD-CCCC-0000-000000000000"),
                    City = "test",
                    Street = "test",
                    ZipCode = "test",
                },
            new Address
            {
                Id = System.Guid.Parse("EEEEEEEE-DDDD-FFFF-0000-000000000000"),
                City = "test2",
                Street = "test2",
                ZipCode = "test2",
            }
            );

            // Add seed ProductTypes
            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Name = "testingCategory"
                },
                new ProductType
                {
                    Name = "testingCategory2"
                });

            var pid1 = Guid.NewGuid();
            var pid2 = Guid.NewGuid();
            var pid3 = Guid.NewGuid();
            var pid4 = Guid.NewGuid();

            // Add seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = pid1,
                    ProductName = "testName",
                    Price = 1,
                    Available = true,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 1,
                },
                new Product
                {
                    Id = pid2,
                    ProductName = "testName2",
                    Price = 3,
                    Available = true,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 2,
                },
                new Product
                {
                    Id = pid3,
                    ProductName = "testName3",
                    Price = 5,
                    Available = false,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 5,
                },
                new Product
                {
                    Id = pid4,
                    ProductName = "testName4",
                    Price = 6,
                    Available = true,
                    ProductTypeFK = "testingCategory",
                    Quantity = 6,
                }); ;

            // Add seed Employee
            modelBuilder.Entity<ShopEmployee>().HasData(
                new ShopEmployee
                {
                    Id = System.Guid.Parse("FFFFFFFF-CCCC-CCCC-0000-000000000000"),
                    CurrentState = CurrentState.Idle,
                    Email = "testmail",
                    EmployedSince = System.DateTime.MinValue,
                    LastName = "testowy",
                    Name = "tester",
                    PhoneNumber = "000-000-000"
                });

            // Add seed Manager
            modelBuilder.Entity<ShopManager>().HasData(
                new ShopManager
                {
                    Id = System.Guid.Parse("FFFFFFFF-CCCC-FFFF-0000-000000000000")
                });

            // Add seed Complaint
            modelBuilder.Entity<Complaint>().HasData(
                new Complaint
                {
                    Id = System.Guid.Parse("FFFFFFFF-AAAA-0000-0000-000000000000"),
                    CurrentStatus = CurrentComplaintState.Pending.ToString(),
                    Text = "test_complaint"
                });

            // Add seed OrderItem
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = System.Guid.Parse("FFFFFFFF-AAAA-CCCC-A000-000000000000"),
                    Currency = "USD",
                    OrderFK = System.Guid.Parse("EEEEEEEE-CCCC-AAAA-0000-000000000000"),
                    ProductFK = pid1,
                    Quantity = 1,
                });

            // Add seed Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = System.Guid.Parse("EEEEEEEE-CCCC-AAAA-0000-000000000000"),
                    AdditionalInfo = "additional",
                    CreationDate = System.DateTime.MinValue,
                    DeliveryDate = System.DateTime.MinValue,
                    AddressFK = System.Guid.Parse("EEEEEEEE-DDDD-FFFF-0000-000000000000")
                });
        }
    }
}
