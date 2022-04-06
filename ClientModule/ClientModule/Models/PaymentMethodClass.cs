using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("PaymentMethod")]
    public class PaymentMethodClass
    {
        //We declare an enum to use in code.
        //It will be a primary key so there will not be multiple of them.
        //Enum will be converted to string
        //See OnModelCreating inside ApplicationDbContext to see how it works exactly.

        //This is not how its supposed to be, TODO change to different classes
        public enum PaymentMethodEnum
        {
            Cash = 0,
            Card = 1
        }
        [Key]
        public PaymentMethodEnum PaymentMethod { get; set; }

    }
}
