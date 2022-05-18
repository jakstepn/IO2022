using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Order")]
    [Index(nameof(Order.Date))]
    public class Order
    {
        [Key]
        public string Id { get; set; } 
        public decimal Price { get; set; }
        public virtual Payment Payment { get; set; }
        public double Discount { get; set; }
        public Address DeliveryAddress { get; set; }
        public DateTime Date { get; set; }
        
        public OrderStatusClass Status { get; set; }
        public List<Product> Products { get; set; }

        public bool AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public bool AddFromCart(ShoppingCart shoppingCart)
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
            return Date.AddDays(4);
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
