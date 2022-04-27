using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("OrderStatus")]
    public class OrderStatusClass
    {
        //We declare an enum to use in code.
        //It will be a primary key so there will not be multiple of them.
        //Enum will be converted to string
        //See OnModelCreating inside ApplicationDbContext to see how it works exactly.
        public enum OrderStatusEnum
        {
            Pending = 0,
            InPreparation,
            ReadyForDelivery,
            PickedUpByCourier,
            RejectedByShop,
            RejectedByCustomer,
            Delivered,
            WaitingForPayment,
            OnTheWay
        }
        [Key]
        public OrderStatusEnum OrderStatus { get; set; }

    }
}
