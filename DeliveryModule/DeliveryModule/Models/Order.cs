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
        public Order(DeliveryModule_ApiClasses.Order ShopOrder)
        {
            this.Id = ShopOrder.orderId;
            RequestedTime = ShopOrder.deliveryDate;
            Price = ShopOrder.orderItems.Sum(x => x.grossPrice);
            IsPaid = false;
            switch (ShopOrder.orderStatus)
            {
                case DeliveryModule_ApiClasses.orderStatus.OneOfPending:
                    SetOrderStatus(Order.OrderStatusEnum.Pending);
                    break;

                case DeliveryModule_ApiClasses.orderStatus.InPreparation:
                    SetOrderStatus(Order.OrderStatusEnum.InPreparation);
                    break;

                case DeliveryModule_ApiClasses.orderStatus.ReadyForDelivery:
                    SetOrderStatus(Order.OrderStatusEnum.ReadyToPickUp);
                    break;

                case DeliveryModule_ApiClasses.orderStatus.RejectedByShop:
                case DeliveryModule_ApiClasses.orderStatus.RejectedByCustomer:
                    SetOrderStatus(Order.OrderStatusEnum.Rejected);
                    break;

                case DeliveryModule_ApiClasses.orderStatus.PickedUpByCourier:
                case DeliveryModule_ApiClasses.orderStatus.Delivered:
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
