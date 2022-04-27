using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientModule_ApiClasses.OrdersModule
{
    
    public class GetAllCurrentOrdersResponse
    {
        public string orderId { get; set; }
        public List<ResponseProduct> orderItems { get; set; } = new();
        public DateTime creationDate { get; set; }
        public DateTime deliveryDate { get; set; }
        public ResponseAddress clientAddress { get; set; }
        public string additionalInfo { get; set; }
        public ResponseOrderStatus orderStatus { get; set; }
        public bool confirmedPayment { get; set; }
    }

}
