using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Models
{
    [DisplayColumn("Order")]
    public class Order
    {
        
        [Key]
        public int Id { get; set; }
        public DateTime RequestedTime { get; set; }
        public string MyProperty { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public Client Client { get; set; }
        public  OrderStatusClass OrderStatus { get; set; }

        public Order():base()
        {
            OrderStatus = new OrderStatusClass();
        }

        public void SetOrderStatus(OrderStatusClass.OrderStatusEnum orderStatus) 
        {
            switch (orderStatus)
            {
                case OrderStatusClass.OrderStatusEnum.InPreparation:
                    OrderStatus.OrderStatus = orderStatus;
                    break;
                case OrderStatusClass.OrderStatusEnum.ReadyToPickUp:
                    OrderStatus.OrderStatus = orderStatus;
                    break;
                case OrderStatusClass.OrderStatusEnum.Rejected:
                    OrderStatus.OrderStatus = orderStatus;
                    break;
                default:
                    break;
            }           
        }
        public void PayOrder(Payment payment) { throw new NotImplementedException(); }
    }
}
