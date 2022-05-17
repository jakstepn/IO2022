using System;
using System.Collections.Generic;

namespace ShopModule_ApiClasses.Messages
{
    public enum OrderStatusMessage {
        WaitingForCollection,
        Collecting,
        WaitingForCourier,
        ParcelCollected,
        OnTheWay,
        Delivered,
        RejectedByShop,
        RejectedByCustomer,
        Pending
    }
    public class OrderMessage
    {
        public Guid orderId { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public AddressMessage clientAddress { get; set; }
        public OrderItemMessage[] orderItems { get; set; }
        public string additionalInfo { get; set; }
        public OrderStatusMessage orderStatus { get; set; }
        public bool confirmedPayment { get; set; }

        public OrderMessage()
        {
        }
    }
}