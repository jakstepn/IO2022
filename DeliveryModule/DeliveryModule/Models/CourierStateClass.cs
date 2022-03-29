using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Database_Models
{
    [DisplayColumn("CourierStatus")]
    public class CourierStatusClass
    {
        //We declare an enum to use in code.
        //It will be a primary key so there will not be multiple of them.
        //Enum will be converted to string
        //See OnModelCreating inside ApplicationDbContext to see how it works exactly.
        public enum CourierStatusEnum
        {
            HasOrder = 0,
            Waiting = 1
        }
        [Key]
        public CourierStatusEnum OrderStatus { get; set; }

    }
}
