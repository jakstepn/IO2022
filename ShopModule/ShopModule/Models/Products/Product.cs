using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Products
{
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

		public virtual Shop Shop { get; set; }
		public virtual ProductType ProductType { get; set; }

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

		// DataBase Relations
		[ForeignKey("Shop")]
		public string ShopFK { get; set; }

		[ForeignKey("ProductType")]
		public string ProductTypeFK { get; set; }
	}
}
