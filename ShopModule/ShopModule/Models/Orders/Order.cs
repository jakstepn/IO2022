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
		public OrderStatus OrderStatus { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		//public Address ClientAddress { get; set; }
		public string AdditionalInfo { get; set; }
		public int ShopEmployeeId { get; set; }
		public int ShopId { get; set; }
		public int CourierId { get; set; }

		public Order()
		{
		}

		public void ChangeStatus(OrderStatus status) { }

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
