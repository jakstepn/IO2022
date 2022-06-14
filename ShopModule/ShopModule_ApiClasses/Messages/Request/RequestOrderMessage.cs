using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_ApiClasses.Messages.Request
{
    public class RequestOrderMessage
    {
        public AddressMessage clientAddress { get; set; }
        public RequestOrderItemMessage[] orderItems { get; set; }
        public string additionalInfo { get; set; }
    }
}
