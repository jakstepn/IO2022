using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using ClientModule.Database_Models;
using ClientModule.Services;
using ClientModule.Data;

using Moq;
using Microsoft.EntityFrameworkCore;

using ClientModule_ApiClasses.OrdersModule;
namespace ClientModule_UnitTests.OrderService
{
    public class GetAllCurrentOrdersUnitTests
    {
        [Fact]
        public void Test()
        {
            var products = new List<Product>
            {
                new Product{ Id = "1", Price = 10, Currency = "PLN", Name = "bread", Quantity = 150},
                new Product{ Id = "1", Price = 2, Currency = "PLN", Name = "cookie", Quantity = 200}

            };
               var data = new List<Order>
            {
                new Order { Status = new OrderStatusClass{ OrderStatus = OrderStatusClass.OrderStatusEnum.Pending },
                Products = products,
                DeliveryAddress = new Address{City ="Warszawa",Street = "Pewna", PostalCode = "123-123" }
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.Setup(c => c.Orders).Returns(mockSet.Object);
            var service = new ClientModule.Services.OrderService(mockContext.Object);


            var orders = service.GetAllCurrentOrders();
            Assert.Equal(2, orders.orderItems.Count);

        }
        
    }
}
