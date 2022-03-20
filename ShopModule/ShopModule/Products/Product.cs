using System;

namespace ShopModule.Products
{
	public class Product
	{
		public decimal Price { get; set; }
		public int TaxRate { get; set; }
		public string ProductName { get; set; }
		public bool Available { get; set; }

		public Product()
		{
		}
	}
}
