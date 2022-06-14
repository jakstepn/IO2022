using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public static class Shop
    {
        public static Order? QueryShopForOrder() 
        {
            return new Order();
        }
        public static void DeclareAvailability(Courier courier) 
        {
            courier.CurrentState = Courier.CourierStatusEnum.AvaibleForDelivery;
        }
        public static void TransferOrderToAnotherCourier(Courier owner, Courier destination) 
        {
            destination.CurrentOrder = owner.CurrentOrder;
            owner.CurrentOrder = null;
            DeclareAvailability(owner);
            destination.CurrentState = Courier.CourierStatusEnum.DuringDelivery;
        } 

    }
}
