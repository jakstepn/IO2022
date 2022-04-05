using DeliveryModule.Models;
using System;
using Xunit;

namespace DeliveryModule_UnitTests
{
    public class UnitTestOrderClass
    {
        [Theory]
        [InlineData(OrderStatusClass.OrderStatusEnum.InPreparation)]
        [InlineData(OrderStatusClass.OrderStatusEnum.ReadyToPickUp)]
        [InlineData(OrderStatusClass.OrderStatusEnum.Rejected)]
        public void TestSetOrderStatus1(OrderStatusClass.OrderStatusEnum inStatus)
        {
            Order order = new Order();
            Assert.Equal(OrderStatusClass.OrderStatusEnum.Pending, order.OrderStatus.OrderStatus);
            order.SetOrderStatus(inStatus);
            Assert.Equal(inStatus, order.OrderStatus.OrderStatus);
        }
    }
}
