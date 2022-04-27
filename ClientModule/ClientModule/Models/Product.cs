using System;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Product")]
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public int Sell(int quantity) 
        {
            throw new NotImplementedException();
        }
        public Address AvailableAt(Address location) 
        {
            throw new NotImplementedException();
        }
    }
}