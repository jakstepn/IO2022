using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;

namespace ShopModule.Orders
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public virtual Guid Id { get; set; }	
		public virtual string OrderStatus { get; set; }
		public virtual DateTime CreationDate { get; set; }
		public virtual DateTime DeliveryDate { get; set; }
		public virtual Address ClientAddress { get; set; }
		public virtual string AdditionalInfo { get; set; }
		public virtual ICollection<OrderItem> Items { get; set; }

		public Order()
		{
		}

		public Order(RequestOrderMessage message, Address a)
        {
			CreationDate = DateTime.Now;
			Id = Guid.NewGuid();
			OrderStatus = Orders.OrderStatus.Pending.ToString();
			AdditionalInfo = message.additionalInfo;
			AddressFK = a.Id;
		}

		public void ChangeStatus(OrderStatus status) => OrderStatus = status.ToString();

        public OrderMessage Convert(IVisitor visitor)
        {
			return visitor.Visit(this);
        }

		[ForeignKey("ClientAddress")]
		public virtual Guid AddressFK { get; set; }
	}
}
