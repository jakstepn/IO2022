using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientModule_ApiClasses.ClientModule
{
    public class ApiClientAddress
    {
        public string street;
        public string city;
        public string zipCode;
    }

    public class ClientDetailsResponse
    {
        public string userId;
        public string phoneNumber;
        public ApiClientAddress clientAddress;
    }
}
