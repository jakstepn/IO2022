using System;
using Xunit;
using ShopModule;
using ShopModule.Orders;
using ShopModule.Products;
using System.Collections;
using System.Collections.Generic;

namespace ShopModule_UnitTests
{
    public class UnitTestOrderClass
    {
        [Fact]
        public void TestDefaultOrderStatus()
        {
            Order o = new Order();
            Assert.Equal(OrderStatus.Pending.ToString(), o.OrderStatus);
        }

        [Theory]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.PickedUpByCourier)]
        [InlineData(OrderStatus.ReadyForDelivery)]
        [InlineData(OrderStatus.RejectedByShop)]
        [InlineData(OrderStatus.RejectedByCustomer)]
        public void TestOrderStatus(OrderStatus status)
        {
            Order o = new Order
            {
                AdditionalInfo = "test",
                AddressFK = Guid.NewGuid(),
                ClientAddress = null,
                CreationDate = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                OrderStatus = OrderStatus.InPreparation.ToString(),
            };
            o.ChangeStatus(status);
            Assert.Equal(status.ToString(), o.OrderStatus);
        }
    }
}
