using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
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
        public virtual decimal GrossPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public virtual decimal Tax { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int Quantity { get; set; }
        public virtual string Currency { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(OrderItemMessage message, Order order, Product product)
        {
            Id = message.orderItemId;
            GrossPrice = message.grossPrice;
            ProductName = message.productName;
            Quantity = message.quantity;
            Currency = message.currency;
            Order = order;
            Product = product;
        }

        // DataBase Relations
        [ForeignKey("Product")]
        public virtual string ProductFK { get; set; }
        [ForeignKey("Order")]
        public virtual Guid OrderFK { get; set; }

    }
}
