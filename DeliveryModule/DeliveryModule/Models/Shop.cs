using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public class Shop
    {
        public Order? QueryShopForOrder(Shop shop) { throw new NotImplementedException(); }
        public void DeclareAvailability(Courier courier) { throw new NotImplementedException(); }
        public void TransferOrderToAnotherCourier(Order order) { throw new NotImplementedException(); } 
    }
}
