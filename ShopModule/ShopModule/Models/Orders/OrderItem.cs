using ShopModule.Products;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Orders
{
    public class OrderItem
    {
        [Key]
        public string Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal GrossPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

        public OrderItem()
        {
        }

        // DataBase Relations
        [ForeignKey("Product")]
        public string ProductFK { get; set; }
        [ForeignKey("Order")]
        public string OrderFK { get; set; }

    }
}
