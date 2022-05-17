using Complaints;
using Microsoft.EntityFrameworkCore;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            // Add seed addreses
            builder.Entity<Address>().HasData(
                new Address
                {
                    Id = System.Guid.Parse("EEEEEEEE-DDDD-CCCC-0000-000000000000"),
                    City = "test",
                    Country = "test",
                    Region = "test",
                    Street = "test",
                    StreetNumber = "test",
                    ZipCode = "test",
                },
            new Address
            {
                Id = System.Guid.Parse("EEEEEEEE-DDDD-FFFF-0000-000000000000"),
                City = "test2",
                Country = "test2",
                Region = "test2",
                Street = "test2",
                StreetNumber = "test2",
                ZipCode = "test2",
            }
            );

            // Add seed ProductTypes
            builder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Name = "testingCategory"
                },
                new ProductType
                {
                    Name = "testingCategory2"
                });

            // Add seed Products
            builder.Entity<Product>().HasData(
                new Product
                {
                    ProductName = "testName",
                    Price = 1,
                    Available = true,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 1,
                    TaxRate = 0,
                },
                new Product
                {

                    ProductName = "testName2",
                    Price = 3,
                    Available = true,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 2,
                    TaxRate = 0
                },
                new Product
                {
                    ProductName = "testName3",
                    Price = 5,
                    Available = false,
                    ProductTypeFK = "testingCategory2",
                    Quantity = 5,
                    TaxRate = 1
                },
                new Product
                {
                    ProductName = "testName4",
                    Price = 6,
                    Available = true,
                    ProductTypeFK = "testingCategory",
                    Quantity = 6,
                    TaxRate = 1
                });

            // Add seed Employee
            builder.Entity<ShopEmployee>().HasData(
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
            builder.Entity<ShopManager>().HasData(
                new ShopManager
                {
                    Id = System.Guid.Parse("FFFFFFFF-CCCC-FFFF-0000-000000000000")
                });

            // Add seed Complaint
            builder.Entity<Complaint>().HasData(
                new Complaint
                {
                    Id = System.Guid.Parse("FFFFFFFF-AAAA-0000-0000-000000000000"),
                    CurrentStatus = Complaints.CurrentComplaintState.Pending,
                    Text = "test_complaint"
                });

            // Add seed OrderItem
            builder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    Id = System.Guid.Parse("FFFFFFFF-AAAA-CCCC-A000-000000000000"),
                    Currency = "USD",
                    OrderFK = System.Guid.Parse("EEEEEEEE-CCCC-AAAA-0000-000000000000"),
                    GrossPrice = 10,
                    ProductFK = "testName",
                    ProductName = "name",
                    Quantity = 1,
                    Tax = 0,
                });

            // Add seed Order
            builder.Entity<Order>().HasData(
                new Order
                {
                    Id = System.Guid.Parse("EEEEEEEE-CCCC-AAAA-0000-000000000000"),
                    AdditionalInfo = "additional",
                    ConfirmedPayment = false,
                    CreationDate = System.DateTime.MinValue,
                    DeliveryDate = System.DateTime.MinValue,
                    AddressFK = System.Guid.Parse("EEEEEEEE-DDDD-FFFF-0000-000000000000")
                });
        }
    }
}
