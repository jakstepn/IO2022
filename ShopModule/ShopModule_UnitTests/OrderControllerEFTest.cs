using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Controllers;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class OrderControllerEFTest
    {
        [Fact]
        public void PlaceOrderTest()
        {
            var mockOrderSet = new Mock<DbSet<Order>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);

            var service = new OrderService(mockContext.Object);

            var testOrder = new Order{ OrderStatus = OrderStatus.WaitingForCollection,
                                      CreationDate = DateTime.MinValue, DeliveryDate = DateTime.Now,
                                      ClientAddress = new Address { City="nonecity", Country="nocountry",
                                                                    Region="noregion", Street="nostreet",
                                                                    StreetNumber="1a", ZipCode="nozipcode" },
                                      AdditionalInfo="none", ShopFK="0", CourierFK="0" };

            mockOrderSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once);
        }
        [Fact]
        public void AddOrderItemsTest()
        {
            var mockOrderItemSet = new Mock<DbSet<OrderItem>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.OrderItems).Returns(mockOrderItemSet.Object);

            var service = new OrderService(mockContext.Object);

            var testItem1 = new OrderItem { GrossPrice = 10, OrderFK = "1",
                ProductId = "1", ProductName = "test", Quantity = 0, Tax = 0 };
            var testItem2 = new OrderItem { GrossPrice = 50, OrderFK = "1",
                ProductId = "2", ProductName = "test1", Quantity = 1, Tax = 0 };
            var testItem3 = new OrderItem { GrossPrice = 60, OrderFK = "2",
                ProductId = "3", ProductName = "test2", Quantity = 2, Tax = 1 };

            service.AddOrderItems(new OrderItem[] { testItem1, testItem2, testItem3 });
            mockOrderItemSet.Verify(m => m.Add(It.IsAny<OrderItem>()), Times.Exactly(3));
        }
        [Fact]
        public void GetPendingOrdersTest()
        {
            var mockOrderSet = new Mock<DbSet<Order>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);

            var service = new OrderService(mockContext.Object);

            var testOrder1 = new Order
            {
                OrderStatus = OrderStatus.Pending,
                CreationDate = DateTime.MinValue,
                DeliveryDate = DateTime.Now,
                ClientAddress = new Address
                {
                    City = "nonecity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "2a",
                    ZipCode = "nozipcode"
                },
                AdditionalInfo = "none",
                ShopFK = "0",
                CourierFK = "2"
            };

            var testOrder2 = new Order
            {
                OrderStatus = OrderStatus.Pending,
                CreationDate = DateTime.MinValue,
                DeliveryDate = DateTime.Now,
                ClientAddress = new Address
                {
                    City = "nonecity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "1a",
                    ZipCode = "nozipcode"
                },
                AdditionalInfo = "none",
                ShopFK = "0",
                CourierFK = "1"
            };

            var testOrder3 = new Order
            {
                OrderStatus = OrderStatus.WaitingForCollection,
                CreationDate = DateTime.MinValue,
                DeliveryDate = DateTime.Now,
                ClientAddress = new Address
                {
                    City = "nonecity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "3a",
                    ZipCode = "nozipcode"
                },
                AdditionalInfo = "none",
                ShopFK = "0",
                CourierFK = "4"
            };

            service.AddOrder(testOrder1);
            service.AddOrder(testOrder2);
            service.AddOrder(testOrder3);

            var pendingOrders = service.FindPendingOrders();

            Assert.True(pendingOrders.Count == 2);
        }
        [Fact]
        public void FindOrderTest()
        {
            var mockOrderSet = new Mock<DbSet<Order>>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);

            var service = new OrderService(mockContext.Object);

            var testOrder1 = new Order
            {
                Id = "test",
                OrderStatus = OrderStatus.Pending,
                CreationDate = DateTime.MinValue,
                DeliveryDate = DateTime.Now,
                ClientAddress = new Address
                {
                    City = "nonecity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "2a",
                    ZipCode = "nozipcode"
                },
                AdditionalInfo = "none",
                ShopFK = "0",
                CourierFK = "2"
            };

            var testOrder2 = new Order
            {
                Id="test2",
                OrderStatus = OrderStatus.Pending,
                CreationDate = DateTime.MinValue,
                DeliveryDate = DateTime.Now,
                ClientAddress = new Address
                {
                    City = "nonecity",
                    Country = "nocountry",
                    Region = "noregion",
                    Street = "nostreet",
                    StreetNumber = "2a",
                    ZipCode = "nozipcode"
                },
                AdditionalInfo = "none",
                ShopFK = "0",
                CourierFK = "2"
            };

            var found = service.FindOrder("test");

            Assert.Equal(testOrder1, found);
        }
    }
}
