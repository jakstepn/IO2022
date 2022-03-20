using System;

namespace Orders
{
	public class Order
	{
		public OrderStatus OrderStatus { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		// TODO
		//public DateTime ClientAddress { get; set; }
		public string AdditionalInfo { get; set; }

		public Order()
		{
		}

		public void ChangeStatus(OrderStatus status) { }
	}
}
