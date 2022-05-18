using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShopModule.Employees;
using ShopModule.Location;
using ShopModule_ApiClasses.Messages;

namespace ShopModule.Orders
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		public virtual Guid Id { get; set; }	
		public virtual OrderStatus OrderStatus { get; set; }
		public virtual DateTime CreationDate { get; set; }
		public virtual DateTime DeliveryDate { get; set; }
		public virtual Address ClientAddress { get; set; }
		public virtual string AdditionalInfo { get; set; }
		public virtual bool ConfirmedPayment { get; set; }
		//public Courier Courier { get; set; }
		public virtual ICollection<OrderItem> Items { get; set; }

		public Order()
		{
		}

		public Order(Guid id, Address client_address, DateTime delivery_date, DateTime creation_date,
			string additional_info = "", OrderStatus order_status = OrderStatus.WaitingForCollection)
        {
			Id = id;
			ClientAddress = client_address;
			DeliveryDate = delivery_date;
			CreationDate = creation_date;
			AdditionalInfo = additional_info;
			OrderStatus = order_status;
        }

		public Order(OrderMessage message)
        {
			Id = message.orderId;
			ConfirmedPayment = message.confirmedPayment;
			OrderStatus = (OrderStatus) message.orderStatus;
			CreationDate = message.creationDate;
			DeliveryDate= message.deliveryDate;
			AdditionalInfo = message.additionalInfo;
			ClientAddress = new Address(message.clientAddress);
		}

		public void ChangeStatus(OrderStatus status) => OrderStatus = status;

        public OrderMessage Convert(IVisitor visitor)
        {
			return visitor.Visit(this);
        }

		[ForeignKey("Address")]
		public virtual Guid AddressFK { get; set; }
	}
}
