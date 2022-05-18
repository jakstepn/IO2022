﻿using ShopModule.Orders;
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
		public virtual Guid Id { get; set; }
		public virtual string Country { get; set; }
		public virtual string City { get; set; }
		public virtual string Region { get; set; }
		public virtual string Street { get; set; }
		// House numbers can contain letters
		public virtual string StreetNumber { get; set; }
		public virtual string ZipCode { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

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