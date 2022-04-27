using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopModule_ApiClasses.Messages
{
	public class AddressMessage
	{
		public string city { get; set; }
		public string street { get; set; }
		public string zipCode { get; set; }

		public AddressMessage()
        {
        }
	}
}