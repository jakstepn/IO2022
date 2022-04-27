using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule.Location
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string Street { get; set; }
		// House numbers can contain letters
		public string StreetNumber { get; set; }
		public string ZipCode { get; set; }
		public ICollection<Order> Orders { get; set; }

		public Address()
        {
        }

		public Address(string country, string region, string city,
			string street, string street_number, string zip_code)
        {
			City = city;
			Region = region;
			Street = street;
			StreetNumber = street_number;
			ZipCode = zip_code;
			Country	= country;
        }

		public Address(AddressMessage message)
        {
			City = message.city;
			Street = message.street;
			ZipCode = message.zipCode;
        }
	}
}