using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("ShoppingCart")]
    public class DbShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<DbShoppingCartEntry> Entries { get; set; }

        public void AddProduct(DbProduct product)
        {
            throw new NotImplementedException();
        }
        public void DeleteProduct(DbProduct product)
        {
            throw new NotImplementedException();
        }
    }
}