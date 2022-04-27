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
    public class RejectOrderUnitTests
    {
        [Theory]
        [InlineData(OrderStatusClass.OrderStatusEnum.InPreparation)]
        [InlineData(OrderStatusClass.OrderStatusEnum.Pending)]
        [InlineData(OrderStatusClass.OrderStatusEnum.PickedUpByCourier)]
        public void CheckIfCanAccept(OrderStatusClass.OrderStatusEnum orderStatus)
        {
            var data = new List<Order>
            {
                new Order { Status = new OrderStatusClass{ OrderStatus = orderStatus },
                Id = "Test_order"
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

            var response = service.RejectOrder("Test_order");
            Assert.Equal(ResponseOrderStatus.RejectedByCustomer, response.orderStatus);
        }

        [Theory]
        [InlineData(OrderStatusClass.OrderStatusEnum.RejectedByCustomer)]
        [InlineData(OrderStatusClass.OrderStatusEnum.RejectedByShop)]
        [InlineData(OrderStatusClass.OrderStatusEnum.Delivered)]
        public void CheckIfRejects(OrderStatusClass.OrderStatusEnum orderStatus)
        {
            var data = new List<Order>
            {
                new Order { Status = new OrderStatusClass{ OrderStatus = orderStatus },
                Id = "Test_order"
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

            var response = service.RejectOrder("Test_order");
            Assert.Equal((ResponseOrderStatus)(int)orderStatus, response.orderStatus);
        }
        [Fact]
        public void CheckIfThrowsNotFound()
        {
            var data = new List<Order>
            {
                new Order { Status = new OrderStatusClass{ OrderStatus =  OrderStatusClass.OrderStatusEnum.WaitingForPayment},
                Id = "Test_order2"
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

            Assert.Throws<KeyNotFoundException>(() => service.RejectOrder("Test_order"));
 
        }
    }
}
