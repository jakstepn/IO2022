using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }


        public int Sell(int quantity) { return 0; }
        public Address AvailableAt(Address location) { return null; }
    }
}
