using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Models
{
    public class Shop
    {
        Order? QueryShopForOrder(Shop shop) { return new Order(); }
        void DeclareAvailability(Courier courier) { }
        public void TransferOrderToAnotherCourier(Order order) { }
    }
}
