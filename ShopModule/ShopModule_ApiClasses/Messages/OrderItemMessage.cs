using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_ApiClasses.Messages
{
    public class OrderItemMessage
    {
        public string currency { get; set; }
        public string productName { get; set; }
        public decimal grossPrice { get; set; }
        public int quantity { get; set; }
    }
}
