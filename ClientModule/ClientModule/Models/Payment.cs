using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class Payment
    {
        public bool InAdvance { get; set; }


        public bool Complete() { return false;  }
    }
}
