using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class Card: Payment
    {
        private string _CVV;
        public string AccountNo;
    }
}
