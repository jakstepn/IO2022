namespace DeliveryModule.Models
{

    public class Payment
    {
        public decimal Amount { get; set; }
    }
    public class Cash:Payment
    {

    }
    public class Card:Payment
    {
        public string CardNumber { get; set; }
    }
}