using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class ShoppingCart
    {
        public List<Product> Products { get; set; }


        public void AddProduct(Product product) 
        {
            throw new NotImplementedException();
        }
        public void DeleteProduct(Product product) 
        {
            throw new NotImplementedException();
        }
    }
}
