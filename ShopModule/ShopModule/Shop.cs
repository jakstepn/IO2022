using System;
using ShopModule.Location;

namespace ShopModule
{
	public class Shop
	{
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public Address Address { get; set; }

		public Shop()
		{
		}

		public bool IsProductAvailable(Product product) { return false; }
	}
}
