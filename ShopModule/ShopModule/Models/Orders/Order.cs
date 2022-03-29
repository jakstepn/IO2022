using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopModule.Employees;
using ShopModule.Location;

namespace ShopModule.Orders
{
	public class Order
	{
		[Key]
		public int Id { get; set; }	
		public OrderStatus OrderStatus { get; private set; }
		public DateTime CreationDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public Address ClientAddress { get; set; }
		public string AdditionalInfo { get; set; }
		public int ShopEmployeeId { get; set; }
		public int ShopId { get; set; }
		public int CourierId { get; set; }

		private Order()
		{
			Id = -1;
			OrderStatus = OrderStatus.Delivered;
			CreationDate = DateTime.Now;
			DeliveryDate = DateTime.Now;
			ClientAddress = new Address();
			AdditionalInfo = string.Empty;
			ShopEmployeeId = -1;
			ShopId = -1;
			CourierId = -1;
		}

		public static Order Empty()
        {
			return new Order();
        }

		public Order(int id, Address client_address, DateTime delivery_date, DateTime creation_date,
			string additional_info = "", OrderStatus order_status = OrderStatus.WaitingForCollection)
        {
			Id = id;
			ClientAddress = client_address;
			DeliveryDate = delivery_date;
			CreationDate = creation_date;
			AdditionalInfo = additional_info;
			OrderStatus = order_status;
        }

		public void ChangeStatus(OrderStatus status) => OrderStatus = status;

		// DataBase relations
		[ForeignKey("ShopEmployeeId")]
		public virtual ShopEmployee ShopEmployee { get; set; }
		[ForeignKey("ShopId")]
		public virtual Shop Shop { get; set; }
		public virtual ICollection<OrderItem> OrderItem { get; set; }
		[ForeignKey("CourierId")]
		public virtual Courier Courier { get; set; }

		
	}
}
