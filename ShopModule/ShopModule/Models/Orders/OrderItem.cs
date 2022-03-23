using ShopModule.Products;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Orders
{
	public class OrderItem
	{
		public int Id { get; set; }
		public decimal GrossPrice { get; set; }
		public decimal Tax { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }
		public int ProductId { get; set; }

		public OrderItem()
		{
		}

		// DataBase Relations
		[ForeignKey("ProductId")]
		public virtual Product Products { get; set; }
	}
}
