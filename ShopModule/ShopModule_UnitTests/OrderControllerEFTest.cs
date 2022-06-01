using Microsoft.EntityFrameworkCore;
using Moq;
using ShopModule.Controllers;
using ShopModule.Data;
using ShopModule.Location;
using ShopModule.Models;
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

            var testOrder = new Order{ OrderStatus = OrderStatus.Pending.ToString(),
                                      CreationDate = DateTime.MinValue, DeliveryDate = DateTime.Now,
                                      ClientAddress = new Address { City="nonecity", Country="nocountry",
                                                                    Region="noregion", Street="nostreet",
                                                                    StreetNumber="1a", ZipCode="nozipcode" },
                                      AdditionalInfo="none"
            };

            mockOrderSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Once);
        }
        [Fact]
        public void AddOrderItemsTest()
        {
            var mockOrderItemSet = new Mock<DbSet<OrderItem>>();
            var mockService = new Mock<IOrderService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.OrderItems).Returns(mockOrderItemSet.Object);

            var service = new OrderService(mockContext.Object);

            var testItem1 = new OrderItem { GrossPrice = 10, OrderFK = Guid.NewGuid(), ProductName = "test", Quantity = 0, Tax = 0 };
            var testItem2 = new OrderItem { GrossPrice = 50, OrderFK = Guid.NewGuid(), ProductName = "test1", Quantity = 1, Tax = 0 };
            var testItem3 = new OrderItem { GrossPrice = 60, OrderFK = Guid.NewGuid(), ProductName = "test2", Quantity = 2, Tax = 1 };

            var items = new OrderItem[] { testItem1, testItem2, testItem3 };

            mockService.Setup(x => x.AddOrderItems(items))
                .Returns(items);

            mockService.Object.AddOrderItems(items);
            mockOrderItemSet.Verify(m => m.Add(It.IsAny<OrderItem>()), Times.Exactly(3));
        }
        [Fact]
        public void GetPendingOrdersTest()
        {
            var mockOrderSet = new Mock<DbSet<Order>>();
            var mockService = new Mock<IOrderService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);

            var service = new OrderService(mockContext.Object);

            var testOrder1 = new Order
            {
                OrderStatus = OrderStatus.Pending.ToString(),
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
            };

            var testOrder2 = new Order
            {
                OrderStatus = OrderStatus.Pending.ToString(),
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
            };

            var testOrder3 = new Order
            {
                OrderStatus = OrderStatus.Pending.ToString(),
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
            };

            mockService.Setup(x => x.AddOrder(testOrder1.Convert(StaticData.defaultConverter)))
                .Returns(testOrder1.Convert(StaticData.defaultConverter));

            mockService.Setup(x => x.AddOrder(testOrder2.Convert(StaticData.defaultConverter)))
               .Returns(testOrder2.Convert(StaticData.defaultConverter));

            mockService.Setup(x => x.AddOrder(testOrder3.Convert(StaticData.defaultConverter)))
               .Returns(testOrder3.Convert(StaticData.defaultConverter));

            mockService.Setup(x => x.FindPendingOrdersPaginated(0,2))
               .Returns(new List<ShopModule_ApiClasses.Messages.OrderMessage> 
               { 
                   testOrder1.Convert(StaticData.defaultConverter) ,
                   testOrder2.Convert(StaticData.defaultConverter)
               });

            mockService.Object.AddOrder(testOrder1.Convert(StaticData.defaultConverter));
            mockService.Object.AddOrder(testOrder2.Convert(StaticData.defaultConverter));
            mockService.Object.AddOrder(testOrder3.Convert(StaticData.defaultConverter));

            var pendingOrders = mockService.Object.FindPendingOrdersPaginated(0, 2);

            Assert.True(pendingOrders.Count == 2);
        }
        [Fact]
        public void FindOrderTest()
        {
            var mockOrderSet = new Mock<DbSet<Order>>();
            var mockService = new Mock<IOrderService>();

            var mockContext = new Mock<ShopModuleDbContext>();
            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);

            var service = new OrderService(mockContext.Object);

            var toFind = Guid.NewGuid();

            var testOrder1 = new Order
            {
                Id = toFind,
                OrderStatus = OrderStatus.Pending.ToString(),
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
            };

            var testOrder2 = new Order
            {
                Id= Guid.NewGuid(),
                OrderStatus = OrderStatus.Pending.ToString(),
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
            };

            mockService.Setup(x => x.FindOrder(toFind))
              .Returns(testOrder1.Convert(StaticData.defaultConverter));


            var found = mockService.Object.FindOrder(toFind);

            Assert.Equal(testOrder1.Convert(StaticData.defaultConverter).orderId, found.orderId);
        }
    }
}
