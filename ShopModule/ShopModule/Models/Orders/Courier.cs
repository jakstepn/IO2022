using System;

namespace ShopModule.Orders
{
	public class Courier
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }

		public Courier()
		{
		}

		public void CheckCourierAvailability() { }
	}
}
