using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("ShoppingCart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<ShoppingCartEntry> Entries { get; set; }


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