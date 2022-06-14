using ShopModule.Generators;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Orders
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        public virtual Guid Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public virtual int Quantity { get; set; }
        public virtual string Currency { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(Order order, Product product, int quantity)
        {
            Id = Guid.NewGuid();
            Quantity = quantity;
            Currency = CurrencyGenerator.GetGenerator().GenerateCurrency();
            Order = order;
            Product = product;
        }

        // DataBase Relations
        [ForeignKey("Product")]
        public virtual Guid ProductFK { get; set; }
        [ForeignKey("Order")]
        public virtual Guid OrderFK { get; set; }

    }
}
