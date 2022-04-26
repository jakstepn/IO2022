using System;

namespace ShopModule.Orders
{
	public enum OrderStatus
	{
		WaitingForCollection,
		Collecting,
		WaitingForCourier,
		ParcelCollected,
		OnTheWay,
		Delivered,
		RejectedByShop,
		RejectedByCustomer,
		Pending
	}
}
