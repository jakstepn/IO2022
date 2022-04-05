using System.ComponentModel.DataAnnotations;

namespace DeliveryModule.Models
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
            Available =0,
            Busy=1,
            AwayFromWork=2
        }
        [Key]
        public CourierStatusEnum CourierStatus { get; set; }

    }
}
