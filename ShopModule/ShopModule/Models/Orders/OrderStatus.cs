using System;

namespace ShopModule.Orders
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
