using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientModule.Data;
using ClientModule.Services;

using System.Linq;
namespace ClientModule.Controllers
{
    [Route("clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [Route("{clientAddress}")]
        public ActionResult GetClientDetails([FromRoute]string clientAddress)
        {
            return new JsonResult(_clientService.GetClientDetails(clientAddress));
        }
    }
}
