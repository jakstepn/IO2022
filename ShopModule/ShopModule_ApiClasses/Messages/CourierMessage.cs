using System;

namespace ShopModule_ApiClasses.Messages
{
    public class CourierMessage
    {
        public Guid courierId { get; set; }
        public Guid orderId { get; set; }
    }
}
