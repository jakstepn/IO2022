using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using ClientModule.Database_Models;
namespace ClientModule.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAllCurrentOrders()
        {
            ActionResult result = new JsonResult("Success");
            return result;
        }

        [HttpGet]
        [Route("history")]
        public ActionResult GetOrdersHistory()
        {
            ActionResult result = new JsonResult("Success");
            return result;
        }

        [HttpGet]
        [Route("{orderId}")]
        public ActionResult GetChosenOrder([FromRoute] string orderId)
        {
            ActionResult result = new JsonResult("Success");
            return result;
        }

        [HttpPut]
        [Route("{orderId}/reject")]
        public ActionResult RejectOrded([FromRoute] string orderId)
        {
            ActionResult result = new JsonResult("Success");
            return result;
        }

        [HttpPut]
        [Route("{orderId}/payment")]
        public ActionResult UpdatePaymentStatus([FromRoute] string orderId)
        {
            ActionResult result = new JsonResult("Success");
            return result;
        }
    }
}
