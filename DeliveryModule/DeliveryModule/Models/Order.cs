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
        public Guid Id { get; set; }
        public DateTime RequestedTime { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public Client Client { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public enum OrderStatusEnum
        {
            Pending = 0,
            InPreparation = 1,
            ReadyToPickUp = 2,
            Delivered = 3,
            Rejected = 4
        }

        

        public void SetOrderStatus(Order.OrderStatusEnum orderStatus) 
        {
            switch (orderStatus)
            {
                case Order.OrderStatusEnum.InPreparation:
                    OrderStatus = orderStatus;
                    break;
                case Order.OrderStatusEnum.ReadyToPickUp:
                    OrderStatus = orderStatus;
                    break;
                case Order.OrderStatusEnum.Rejected:
                    OrderStatus = orderStatus;
                    break;
                default:
                    break;
            }           
        }
        public void PayOrder(Payment payment) { throw new NotImplementedException(); }
    }
}
