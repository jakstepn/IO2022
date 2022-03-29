﻿using System;

namespace ShopModule.Location
{
	public struct Address
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string Region { get; set; }
		public string Street { get; set; }
		public int StreetNumber { get; set; }
		public string ZipCode { get; set; }

		public Address()
        {
			City = "";
			Region = "";
			Street = "";
			StreetNumber = -1;
			ZipCode = "";
			Country	= "";
        }

		public Address(string country, string region, string city,
			string street, int street_number, string zip_code)
        {
			City = city;
			Region = region;
			Street = street;
			StreetNumber = street_number;
			ZipCode = zip_code;
			Country	= country;
        }
	}
}