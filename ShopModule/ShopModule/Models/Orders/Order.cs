using System;
using ShopModule.Location;

namespace ShopModule.Orders
{
	public class Order
	{
		public OrderStatus OrderStatus { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public Address ClientAddress { get; set; }
		public string AdditionalInfo { get; set; }

		public Order()
		{
		}

		public void ChangeStatus(OrderStatus status) { }
	}
}
