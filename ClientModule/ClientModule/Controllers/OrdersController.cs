using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using ClientModule.Database_Models;
using ClientModule.Services;`
namespace ClientModule.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _ordersService;

        public OrdersController(IOrderService service)
        {
            _ordersService = service;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAllCurrentOrders()
        {
            return _ordersService.GetAllCurrentOrders(); ;
        }

        [HttpGet]
        [Route("history")]
        public ActionResult GetOrdersHistory()
        {
            return _ordersService.GetOrdersHistory();
        }

        [HttpGet]
        [Route("{orderId}")]
        public ActionResult GetChosenOrder([FromRoute] string orderId)
        {
            return _ordersService.GetChosenOrder(orderId);
        }

        [HttpPut]
        [Route("{orderId}/reject")]
        public ActionResult RejectOrder([FromRoute] string orderId)
        {
            return _ordersService.RejectOrder(orderId);
        }

        [HttpPut]
        [Route("{orderId}/payment")]
        public ActionResult UpdatePaymentStatus([FromRoute] string orderId)
        {
            return _ordersService.UpdatePaymentStatus(orderId);
        }
    }
}
