using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientModule_ApiClasses.OrdersModule
{

    public class ResponseProduct
    {
        public string orderItemId { get; set; }
        public decimal grossPrice { get; set; }
        public string currency { get; set; }
        public string productName { get; set; }
        public decimal quantity { get; set; }
    }

    public class ResponseAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
    }
    public enum ResponseOrderStatus
    {
        Pending = 0,
        InPreparation,
        ReadyForDelivery,
        PickedUpByCourier,
        RejectedByShop,
        RejectedByCustomer,
        Delivered
    }
}
