using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopModule.Location
{
	[Table("Addresses")]
	public class Address
	{
		[Key]
		public virtual Guid Id { get; set; }
		public virtual string City { get; set; }
		public virtual string Street { get; set; }
		public virtual string ZipCode { get; set; }
		public virtual ICollection<Order> Orders { get; set; }

		public Address()
        {
        }

		public Address(AddressMessage message)
        {
			Id = Guid.NewGuid();
			City = message.city;
			Street = message.street;
			ZipCode = message.zipCode;
        }
	}
}