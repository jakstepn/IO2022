using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientModule.Models
{
    public class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Address> Addresses { get; set; }


        public void MakeOrder(Order order) { }
        public void MakeComplaint(Complaint complaint) { }
    }
}
