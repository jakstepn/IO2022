using ShopModule.Location;
using ShopModule.Orders;
using System;
using System.Collections.Generic;

namespace Models
{
    public class ResponseOrder
    {
        public int orderId { get; set; }
        public Array orderItems { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public Address clientAddress { get; set; }
        public string additionalInfo { get; set; }
        public OrderStatus orderStatus { get; set; }
        public bool confirmedPayment { get; set; }

        public ResponseOrder(Order order, Array orderItems, bool confirmedPayment)
        {
            this.orderItems = orderItems;
            orderId = order.Id;
            creationDate = order.CreationDate;
            deliveryDate = order.DeliveryDate;
            clientAddress = order.ClientAddress;
            additionalInfo = order.AdditionalInfo;
            orderStatus = order.OrderStatus;
            this.confirmedPayment = confirmedPayment;
        }
    }
}