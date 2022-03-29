using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Database_Models
{
    public class Client
    {
        [Key]
        public int id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
