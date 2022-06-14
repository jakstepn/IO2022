using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_ApiClasses.Messages.Request
{
    public class RequestProductMessage
    {
        public decimal price { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int quantity { get; set; }
    }
}
