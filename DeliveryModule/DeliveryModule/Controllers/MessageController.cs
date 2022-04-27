using DeliveryModule.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
        public ActionResult MessageToClient([FromRoute] Guid clientId, [FromBody] Message message)
        {
            bool success = SendMessage(Routs.ClientInterface, message);
            if (!success)
            {
                return NotFound();
            }
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok();

        }
        
        [HttpPost("/message/courier/{courierId}")]
        public ActionResult MessageToCourier([FromRoute] Guid courierId, [FromBody] Message message)
        {
            bool success = SendMessage(Routs.ShopWorkerInterface, message);
            if (!success)
            {
                return NotFound();
            }
            _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok();
        }

        private bool SendMessage(string BaseAddress, Message message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseAddress);

                var requestContent = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");
                var response = client.PostAsync("/Message", requestContent).Result;


                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
