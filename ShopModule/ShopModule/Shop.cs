using System;

namespace ShopModule
{
	public class Shop
	{
		// TODO Address
		public string PhoneNumber { get; set; }
		public string Email { get; set; }

		public Shop()
		{
		}

		public bool IsProductAvailable(Product product) { return false; }
	}
}
