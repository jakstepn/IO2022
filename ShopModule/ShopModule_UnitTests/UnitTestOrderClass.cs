using System;
using Xunit;
using ShopModule;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule_UnitTests
{
    public class UnitTestOrderClass
    {
        [Fact]
        public void TestDefaultOrderStatus()
        {
            Order o = new Order();
            Assert.Equal(OrderStatus.Delivered, o.OrderStatus);
        }

        [Theory]
        [InlineData(OrderStatus.Collecting)]
        [InlineData(OrderStatus.WaitingForCollection)]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.WaitingForCourier)]
        [InlineData(OrderStatus.RejectedByCustomer)]
        [InlineData(OrderStatus.OnTheWay)]
        [InlineData(OrderStatus.RejectedByShop)]
        public void TestOrderStatus(OrderStatus status)
        {
            OrderStatus defStatus = OrderStatus.Collecting;
            Order o = new Order(0, null, DateTime.Now, DateTime.Now, "", defStatus);
            o.ChangeStatus(status);
            Assert.Equal(status, o.OrderStatus);
        }

        [Fact]
        public void TestDefaultCourierStatus()
        {
            Courier courier = new Courier();
            Assert.Equal(CourierCurrentState.Away_from_work,
                courier.CheckCourierAvailability());
        }

        [Fact]
        public void TestShopProductAvability()
        {
            Product p = new Product();
            p.Available = true;
            Shop shop = new Shop();
            Assert.True(shop.IsProductAvailable(p));
        }
    }
}
