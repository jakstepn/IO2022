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
        public string Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal GrossPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Tax { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(OrderItemMessage message)
        {
            Id = message.orderItemId;
            GrossPrice = message.grossPrice;
            ProductName = message.productName;
            Quantity = message.quantity;
            Currency = message.currency;
        }

        // DataBase Relations
        [ForeignKey("Product")]
        public string ProductFK { get; set; }
        [ForeignKey("Order")]
        public string OrderFK { get; set; }

    }
}
