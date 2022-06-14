using ShopModule_ApiClasses.Messages.Request;
using System;

namespace ShopModule_ApiClasses.Messages
{
    public class ProductMessage : RequestProductMessage
    {
        public Guid productId { get; set; }
    }
}
