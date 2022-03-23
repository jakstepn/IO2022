using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Order")]
    [Index(nameof(DbOrder.Date))]
    [Index(nameof(DbOrder.Status))]
    public class DbOrder
    {
        [Key]
        public int Id { get; set; } 
        public decimal Price { get; set; }
        public virtual DbPayment Payment { get; set; }
        public double Discount { get; set; }
        public DbAddress DeliveryAddress { get; set; }
        public DateTime Date { get; set; }
        
        public DbOrderStatus Status { get; set; }

        public bool AddProduct(DbProduct product)
        {
            throw new NotImplementedException();
        }
        public bool AddFromCart(DbShoppingCart shoppingCart)
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

        public DateTime EstimateDeliveryTime()
        {
            throw new NotImplementedException();
        }
        public void NotifyClient()
        {
            throw new NotImplementedException();
        }

        //I do not understand the point of this
        public DbAddress TrackOrder(DateTime date)
        {
            throw new NotImplementedException();
        }

    }
}
