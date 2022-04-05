using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
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
            InPreparation = 1,
            ReadyToPickUp=2,
            Delivered=3,
            Rejected=4
        }
        [Key]
        public OrderStatusEnum OrderStatus { get; set; }
        public OrderStatusClass()
        {
            OrderStatus = OrderStatusEnum.Pending;
        }

    }
}
