using System;
using System.ComponentModel.DataAnnotations;

namespace ClientModule.Database_Models
{
    [DisplayColumn("Product")]
    public class DbProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int Sell(int quantity)
        {
            throw new NotImplementedException();
        }
        //Unclear what it does, but i think it mean you look for a place a product is avaiable at closes to location given.
        public DbAddress AvaiableAt(DbAddress location)
        {
            throw new NotImplementedException();
        }
    }
}