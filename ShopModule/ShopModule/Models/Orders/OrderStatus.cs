using System;

namespace ShopModule.Orders
{
	public enum OrderStatus
	{
		WaitingForCollection,
		WaitingForCourier,
		Collecting,
		OnTheWay,
		RejectedByShop,
		RejectedByCustomer,
		Delivered,
		Pending
	}
}
