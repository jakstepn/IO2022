using System;
using System.Collections.Generic;

namespace ShopModule_ApiClasses.Messages
{
    public enum OrderStatusMessage {
        Pending,
        InPreparation,
        ReadyForDelivery,
        PickedUpByCourier,
        RejectedByShop,
        RejectedByCustomer,
        Delivered
    }
    public class OrderMessage
    {
        public Guid orderId { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public AddressMessage clientAddress { get; set; }
        public OrderItemMessage[] orderItems { get; set; }
        public string additionalInfo { get; set; }
        public string orderStatus { get; set; }

        public OrderMessage()
        {
        }
    }
}