using ShopModule.Location;
using ShopModule.Orders;
using System;
using System.Collections.Generic;

namespace Models.Messages
{
    public class OrderMessage
    {
        public string orderId { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public Address clientAddress { get; set; }
        public OrderItem[] orderItems { get; set; }
        public string additionalInfo { get; set; }
        public OrderStatus orderStatus { get; set; }
        public bool confirmedPayment { get; set; }

        public OrderMessage()
        {
        }
    }
}