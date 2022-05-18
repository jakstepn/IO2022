using System;

namespace ShopModule_ApiClasses.Structures
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
