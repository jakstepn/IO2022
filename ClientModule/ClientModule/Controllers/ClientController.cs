using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientModule.Data;

using System.Linq;
namespace ClientModule.Controllers
{
    [Route("clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{clientAddress}")]
        public ActionResult GetClientDetails([FromRoute]string clientAddress)
        {
            var result = new JsonResult("Success!");
            return result;
        }
    }
}
