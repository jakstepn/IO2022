using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class Complaint
    {
        public string Text { get; set; }
        public Order.Status CurrentState { get; set; }
    }
}
