using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    
    public class Order
    {
        public enum Status { WaitingForPayment, OnTheWay, Delivered }

        public decimal Price { get; set; }
        public Payment PaymentOpt { get; set; }
        public double Discount { get; set; }
        public Address DeliveryAddress { get; set; }
        public DateTime Date { get; set; }
        public Status CurrentState { get; set; }
        public int CourierID { get; set; }

    
        public bool AddProduct(Product item) { return false; }
        public bool AddFromCart(ShoppingCart cart) { return false; }
        public bool ApplyCoupon() { return false; }
        public Courier FindCourier() { return new(); }
        public DateTime EstimateDeliveryDate() { return new(); }
        public void NotifyClient() { }
        public Address TrackOrder(DateTime date) { return new(); }

    }
}
