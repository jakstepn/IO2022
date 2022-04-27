namespace ShopModule_ApiClasses.Messages
{
    public class ProductMessage
    {
        public decimal price { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int quantity { get; set; }

        public ProductMessage()
        {
        }
    }
}
