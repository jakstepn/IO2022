using ShopModule.Employees;
using ShopModule.Orders;
using ShopModule.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class UnitTestShopEmployeeClass
    {
        [Theory]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.PickedUpByCourier)]
        [InlineData(OrderStatus.ReadyForDelivery)]
        [InlineData(OrderStatus.RejectedByShop)]
        [InlineData(OrderStatus.RejectedByCustomer)]
        public void TestChangeOrderStatus(OrderStatus status)
        {
            ShopEmployee e = new ShopEmployee();
            Order o = new Order();
            e.ChangeOrderStatus(o, status);
            Assert.Equal(status.ToString(), o.OrderStatus);
        }

        [Theory]
        [InlineData(OrderStatus.Delivered)]
        [InlineData(OrderStatus.InPreparation)]
        [InlineData(OrderStatus.PickedUpByCourier)]
        [InlineData(OrderStatus.ReadyForDelivery)]
        [InlineData(OrderStatus.RejectedByShop)]
        [InlineData(OrderStatus.RejectedByCustomer)]
        public void TestGetOrderStatus(OrderStatus status)
        {
            ShopEmployee e = new ShopEmployee();
            Order o = new Order();
            o.ChangeStatus(status);
            Assert.Equal(status, e.GetOrderStatus(o));
        }

        [Fact]
        public void TestRejectOrder()
        {
            Order o = new Order();
            ShopEmployee e = new ShopEmployee();
            e.RejectOrder(o);
            Assert.Equal(OrderStatus.RejectedByShop.ToString(), o.OrderStatus);
        }

        [Fact]
        public void TestProductAvability()
        {
            Product p = new Product();
            ShopEmployee e = new ShopEmployee();
            e.SetProductAsUnavailable(p);
            Assert.False(p.Available);
        }
    }
}
