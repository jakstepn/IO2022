using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule
{
	public class Shop
	{
		[Key]
		public string Id { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public Address Address { get; set; }

		public Shop()
		{
		}

		public Shop(string id, string phone_number, string email, Address address)
        {
			Id = id;
			PhoneNumber = phone_number;
			Email = email;
			Address = address;
        }

		public bool IsProductAvailable(Product product)
        {
			bool available = false;
            foreach (var item in Products)
            {
				if(item.Equals(product))
                {
					available = item.Available;
					break;
                }
            }
			return available;
        }

		// Database relations
		public virtual ICollection<ShopEmployee> ShopEmoployees { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<Product> Products { get; set; }
	}
}
