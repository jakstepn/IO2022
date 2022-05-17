using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Products
{
	[Table("Products")]
	public class Product
	{
		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }
		public int TaxRate { get; set; }
        [Key]
		public string ProductName { get; set; }
		public bool Available { get; set; }
		public int Quantity { get; set; }
		public ProductType ProductType { get; set; }
		public ICollection<OrderItem> OrdersItems { get; set; }
		public Product()
		{
		}

		public Product(ProductMessage message, ProductType category)
        {
			Price = message.price;
			Quantity = message.quantity;
			ProductType = category;
			ProductName = message.name;
			Available = Quantity > 0;
        }

		public ProductMessage Convert(IVisitor visitor)
        {
			return visitor.Visit(this);
        }

		[ForeignKey("ProductType")]
		public string ProductTypeFK { get; set; }
	}
}
