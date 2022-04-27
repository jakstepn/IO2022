using System;
using System.Collections.Generic;

namespace DeliveryModule_ApiClasses
{
    public class OrderItem
    {
        public string orderItemId { get; set; }
        public int grossPrice { get; set; }
        public string currency { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
    }

    public class ClientAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
    }

    public class Order
    {
        public Guid orderId { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public ClientAddress clientAddress { get; set; }
        public string additionalInfo { get; set; }
        public orderStatus orderStatus { get; set; }
        public bool confirmedPayment { get; set; }
        
    }
    public enum orderStatus {OneOfPending, InPreparation, ReadyForDelivery, PickedUpByCourier, RejectedByShop, RejectedByCustomer, Delivered}
}
