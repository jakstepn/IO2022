namespace ShopModule.Messages
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

        public ProductMessage(string name, decimal price, string category, int quantity)
        {
            this.name = name;
            this.price = price;
            this.category = category;
            this.quantity = quantity;
        }
    }
}
