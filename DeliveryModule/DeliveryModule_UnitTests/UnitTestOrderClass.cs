using DeliveryModule.Models;
using System;
using Xunit;

namespace DeliveryModule_UnitTests
{
    public class UnitTestOrderClass
    {
        [Theory]
        [InlineData(Order.OrderStatusEnum.InPreparation)]
        [InlineData(Order.OrderStatusEnum.ReadyToPickUp)]
        [InlineData(Order.OrderStatusEnum.Rejected)]
        public void TestSetOrderStatus1(Order.OrderStatusEnum inStatus)
        {
            Order order = new Order();
            Assert.Equal(Order.OrderStatusEnum.Pending, order.OrderStatus);
            order.SetOrderStatus(inStatus);
            Assert.Equal(inStatus, order.OrderStatus);
        }


        
    }
}
