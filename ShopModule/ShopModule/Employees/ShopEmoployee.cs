using System;
using ShopModule.Orders;
using ShopModule.Products;

namespace ShopModule.Employees
{
	public class ShopEmployee
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime EmployedSince { get; set; }
		public CurrentState CurrentState { get; set; }

		public ShopEmployee()
		{
		}

		public void ChangeOrderStatus(Order order, OrderStatus status) { }
		public OrderStatus GetOrderStatus(Order order) { }
		public void RejectOrder(Order order) { }
		public void SetProductAsUnavailable(Product product) { }
		public void NotifyCourierThatPackageIsReady(Courier courier) { }
	}
}
