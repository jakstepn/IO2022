using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule.Orders
{
	public class Courier
	{
		[Key]
		public string Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }

		private CourierCurrentState CurrentState { get; set; }
		
		public Courier()
		{
		}

		public Courier(string id, string email, string first_name, string last_name, string phone_number)
        {
			Id = id;
			Email = email;
			FirstName = first_name;
			LastName = last_name;
			PhoneNumber = phone_number;
			CurrentState = CourierCurrentState.Away_from_work;
		}

		public CourierCurrentState CheckCourierAvailability() => CurrentState;

		// DataBase relations
		public virtual ICollection<Order> Orders { get; set; }
	}
}
