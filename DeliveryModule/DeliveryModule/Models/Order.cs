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
        public Order()
        {

        }
        public Order(ShopModule_ApiClasses.Messages.OrderMessage ShopOrder)
        {
            
            this.Id = ShopOrder.orderId;
            RequestedTime = ShopOrder.deliveryDate;
            Price = ShopOrder.orderItems.Sum(x => x.grossPrice);
            IsPaid = false;
            switch (Enum.Parse(typeof(ShopModule_ApiClasses.Messages.OrderStatusMessage), ShopOrder.orderStatus))
            {
                case ShopModule_ApiClasses.Messages.OrderStatusMessage.Pending:
                    SetOrderStatus(Order.OrderStatusEnum.Pending);
                    break;

                case ShopModule_ApiClasses.Messages.OrderStatusMessage.InPreparation:
                    SetOrderStatus(Order.OrderStatusEnum.InPreparation);
                    break;

                case ShopModule_ApiClasses.Messages.OrderStatusMessage.ReadyForDelivery:
                    SetOrderStatus(Order.OrderStatusEnum.ReadyToPickUp);
                    break;

                case ShopModule_ApiClasses.Messages.OrderStatusMessage.RejectedByShop:
                case ShopModule_ApiClasses.Messages.OrderStatusMessage.RejectedByCustomer:
                    SetOrderStatus(Order.OrderStatusEnum.Rejected);
                    break;

                case ShopModule_ApiClasses.Messages.OrderStatusMessage.Delivered:
                    SetOrderStatus(Order.OrderStatusEnum.Delivered);
                    break;
            }
        }

        public void SetOrderStatus(Order.OrderStatusEnum orderStatus) 
        {
            this.OrderStatus = orderStatus;
        }
        public void PayOrder(Payment payment) { throw new NotImplementedException(); }
    }
}
