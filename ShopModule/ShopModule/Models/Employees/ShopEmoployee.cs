using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Employees
{
	public class ShopEmployee
	{
		[Key]
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime EmployedSince { get; set; }
		public CurrentState CurrentState { get; set; }
		public int ShopId { get; set; }

		public ShopEmployee()
		{
		}

		public void ChangeOrderStatus(Order order, OrderStatus status) { }
		public OrderStatus GetOrderStatus(Order order) { return OrderStatus.Collecting; }
		public void RejectOrder(Order order) { }
		public void SetProductAsUnavailable(Product product) { }
		public void NotifyCourierThatPackageIsReady(Courier courier) { }


		// Database relations
		[ForeignKey("ShopId")]
		public virtual Shop Shop { get; set; }
		public virtual ICollection< Order> Orders { get; set; }

	}
}
