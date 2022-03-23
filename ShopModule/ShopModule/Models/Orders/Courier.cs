using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule.Orders
{
	public class Courier
	{
		[Key]
		public int Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		

		public Courier()
		{
		}

		public void CheckCourierAvailability() { }


		// DataBase relations
		public virtual ICollection<Order> Orders { get; set; }
	}
}
