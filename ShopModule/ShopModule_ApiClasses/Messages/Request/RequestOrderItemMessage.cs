using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_ApiClasses.Messages.Request
{
    public class RequestOrderItemMessage
    {
        public Guid productId { get; set; }
        public int quantity { get; set; }
    }
}
