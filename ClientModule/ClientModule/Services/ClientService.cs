using ClientModule_ApiClasses.ClientModule;
using ClientModule.Data;
using System.Linq;
using System.Collections.Generic;

namespace ClientModule.Services
{
    public interface IClientService
    {
        public ClientDetailsResponse GetClientDetails(string clientAddress);
    }

    public class ClientService : IClientService
    {
        IApplicationDbContext _context;
        public ClientService(IApplicationDbContext context)
        {
            _context = context;
        }
        public ClientDetailsResponse GetClientDetails(string clientAddress)
        {
            var client = _context.Clients.Where(c => c.EmailAddress == clientAddress).FirstOrDefault();
            if(client == null)
            {
                throw new KeyNotFoundException();
            }

            return new ClientDetailsResponse
            {
                userId = client.Id,
                clientAddress = new ApiClientAddress { street = client.Address.Street, city = client.Address.City, zipCode = client.Address.PostalCode },
                phoneNumber = client.PhoneNumber
            };
        }
    }
}
