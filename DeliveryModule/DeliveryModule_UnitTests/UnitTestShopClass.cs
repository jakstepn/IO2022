using DeliveryModule.Models;
using System;
using Xunit;

namespace DeliveryModule_UnitTests
{
    public class UnitTestShopClass
    {
        [Fact]
        public void TestSetTransferOrderToAnotherCourier1()
        {
            Order order = new Order();
            Courier owner = new Courier(), destiantion = new Courier();



            Assert.Equal(Courier.CourierStatusEnum.Available, destiantion.CurrentState);
            Assert.Equal(Courier.CourierStatusEnum.Available, owner.CurrentState);
            Assert.Null(owner.CurrentOrder);
            Assert.Null(destiantion.CurrentOrder);
        }
        [Fact]
        public void TestSetTransferOrderToAnotherCourier2()
        {
            Order order = new Order();
            Courier owner = new Courier(), destiantion = new Courier();

            owner.CurrentState = Courier.CourierStatusEnum.Busy;
            owner.CurrentOrder = order;


            Assert.Equal(Courier.CourierStatusEnum.Busy, owner.CurrentState);
            Assert.Equal(order, owner.CurrentOrder);

        }
        [Fact]
        public void TestSetTransferOrderToAnotherCourier3()
        {
            Order order = new Order();
            Courier owner = new Courier(), destiantion = new Courier();

            owner.CurrentState = Courier.CourierStatusEnum.Busy;
            owner.CurrentOrder = order;

            Shop.TransferOrderToAnotherCourier(owner, destiantion);
            Assert.Equal(Courier.CourierStatusEnum.Available, owner.CurrentState);
            Assert.Null(owner.CurrentOrder);

        }
        [Fact]
        public void TestSetTransferOrderToAnotherCourier4()
        {
            Order order = new Order();
            Courier owner = new Courier(), destiantion = new Courier();

            owner.CurrentState = Courier.CourierStatusEnum.Busy;
            owner.CurrentOrder = order;

            Shop.TransferOrderToAnotherCourier(owner, destiantion);
            Assert.Equal(Courier.CourierStatusEnum.Busy, destiantion.CurrentState);
            Assert.Equal(order,destiantion.CurrentOrder);
        }
        [Fact]
        public void TestSetDeclareAvailability()
        {
            Courier courier = new Courier();

            courier.CurrentState = Courier.CourierStatusEnum.Busy;
            Assert.Equal(Courier.CourierStatusEnum.Busy, courier.CurrentState);

            Shop.DeclareAvailability(courier);

            Assert.Equal(Courier.CourierStatusEnum.Available, courier.CurrentState);

        }
    }
}
