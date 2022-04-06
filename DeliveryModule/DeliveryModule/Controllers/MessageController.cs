using DeliveryModule.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliveryModule.Controllers
{
    public class MessageController : Controller
    {
        private readonly DeliveryModuleDbContext _context;

        public MessageController(DeliveryModuleDbContext context)
        {
            _context = context;

        }
        
        
        [HttpPost("/message/client/{clientId}")]
        public void MessageToClient([FromRoute] Guid clientId,[FromBody] Guid sender, [FromBody] string subject, [FromBody] string body)
        {
            //todo display message to client
        }
        [HttpPost("/message/courier/{courierId}")]
        public void MessageToCourier([FromRoute] Guid courierId, [FromBody] Guid sender, [FromBody] string subject, [FromBody] string body)
        {
            //todo display message to courier
        }
    }
}
