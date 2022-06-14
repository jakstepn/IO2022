using System;

namespace ShopModule_ApiClasses.Structures
{
	public enum OrderStatus
	{
        Pending,
        InPreparation,
        ReadyForDelivery,
        PickedUpByCourier,
        RejectedByShop,
        RejectedByCustomer,
        Delivered
    }
}
