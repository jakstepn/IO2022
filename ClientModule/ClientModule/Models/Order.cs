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

    
        public bool AddProduct(Product item) 
        { 
            throw new NotImplementedException(); 
        }
        public bool AddFromCart(ShoppingCart cart) 
        { 
            throw new NotImplementedException(); 
        }
        public bool ApplyCoupon() 
        { 
            throw new NotImplementedException(); 
        }
        public Courier FindCourier() 
        { 
            throw new NotImplementedException(); 
        }
        public DateTime EstimateDeliveryDate() 
        { 
            throw new NotImplementedException(); 
        }
        public void NotifyClient() 
        { 
            throw new NotImplementedException(); 
        }
        public Address TrackOrder(DateTime date) 
        { 
            throw new NotImplementedException();
        }

    }
}
