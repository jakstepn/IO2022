using System;
using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
{
    public class Shop
    {
        [Key]
        public Guid Id { get; set; }
        public Order? QueryShopForOrder(Shop shop) { throw new NotImplementedException(); }
        public void DeclareAvailability(Courier courier) 
        {
            
        }
        public void TransferOrderToAnotherCourier(Order order) { throw new NotImplementedException(); } 

    }
}
