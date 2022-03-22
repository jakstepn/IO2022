using System;

namespace ShopModule.Orders
{
	public class OrderItem
	{
		public decimal GrossPrice { get; set; }
		public decimal Tax { get; set; }
		public string ProductName { get; set; }
		public int Quantity { get; set; }

		public OrderItem()
		{
		}
	}
}
