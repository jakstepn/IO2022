using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("OrderStatus")]
    public class DbOrderStatus
    {
        //We declare an enum to use in code.
        //It will be a primary key so there will not be multiple of them.
        //Enum will be converted to string
        //See OnModelCreating inside ApplicationDbContext to see how it works exactly.
        public enum OrderStatusEnum
        {
            WaitingForPayment = 0,
            OnTheWay = 1,
            Delivered = 2
        }
        [Key]
        public OrderStatusEnum OrderStatus { get; set; }

    }
}
