using ShopModule.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Products
{
	public class Product
	{
		[Key]
		public int Id { get; set; }	
		public decimal Price { get; set; }
		public int TaxRate { get; set; }
		public string ProductName { get; set; }
		public bool Available { get; set; }
		public int ShopId { get; set; }

		public Product()
		{
		}

		// DataBase Relations
		public virtual ICollection<OrderItem> OrderItems { get; set; }
		[ForeignKey("ShopId")]
		public virtual Shop Shop { get; set; }
	}
}
