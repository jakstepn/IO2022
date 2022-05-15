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
		[Key]
		public string Id { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal Price { get; set; }
		public int TaxRate { get; set; }
		public string ProductName { get; set; }
		public bool Available { get; set; }
		public int Quantity { get; set; }
		public ProductType ProductType { get; set; }
		public ICollection<Order> Orders { get; set; }
		public Product()
		{
		}

		public Product(ProductMessage message)
        {
			Price = message.price;
			Quantity = message.quantity;
			ProductTypeFK = message.category;
			ProductName = message.name;
        }

		[ForeignKey("ProductType")]
		public string ProductTypeFK { get; set; }
	}
}
