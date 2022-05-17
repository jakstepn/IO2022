using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Orders
{
	[Table("Couriers")]
	public class Courier
	{
		[Key]
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }

		private CourierCurrentState CurrentState { get; set; }
		
		public Courier()
		{
		}

		public Courier(Guid id, string email, string first_name, string last_name, string phone_number)
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
		public ICollection<Order> Orders { get; set; }
	}
}
