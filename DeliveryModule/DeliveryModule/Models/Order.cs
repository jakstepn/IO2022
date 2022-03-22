using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Models
{
    public class Order
    {
        public DateTime RequestedTime { get; set; }
        public string MyProperty { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public Client Client { get; set; }
        void SetOrderStatus(string Status) { }
        void PatOrder(Payment payment) { }
    }
    
}
