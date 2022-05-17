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
		public Guid Id { get; set; }	
		public OrderStatus OrderStatus { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime DeliveryDate { get; set; }
		public Address ClientAddress { get; set; }
		public string AdditionalInfo { get; set; }
		public bool ConfirmedPayment { get; set; }
		//public Courier Courier { get; set; }
		public ICollection<OrderItem> Items { get; set; }

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
		public Guid AddressFK { get; set; }
	}
}
