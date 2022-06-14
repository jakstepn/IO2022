using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryModule.Models
{
    public class Client
    {
        [Key]
        public Guid id { get; set; }
        public string PhoneNumber { get; set; }
        public void SendMessage(string Message)
        {

        }
        public Client()
        {

        }
    }
}
