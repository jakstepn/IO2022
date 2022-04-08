using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Orders;
namespace ShopModule.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OverController : Controller
    {
        private readonly ShopModuleDbContext _context;

        public OverController(ShopModuleDbContext context)
        {
            _context = context;
        }
        [HttpPost("/orders/place")]
        public IActionResult PlaceOrder([FromBody] OrderItem item)
        {
            bool orderCreated = false;

            // Adding an OrderItem to the database
            _context.OrderItem.Add(item);
            if(_context.SaveChanges() == 1)
            {
                orderCreated = true;
            }

            if (orderCreated)
            {
                var res = new JsonResult(item);
                res.StatusCode = 201;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to create an order");
                res.StatusCode = 404;
                return res;
            }
        }

        [HttpGet("/orders/pending/{shopId}")]
        public IActionResult GetPendingOrdersAssignedToShop([FromRoute] int shopID)
        {
            Shop shop = _context.Shop.Find(shopID);
            Order pending = null;

            if (shop != null)
            {
                foreach (var order in shop.Orders)
                {
                    if(order.OrderStatus == Orders.OrderStatus.WaitingForCollection)
                    {
                        pending = order;
                        break;
                    }
                }
                var res = pending != null ? new JsonResult(pending) :
                    new JsonResult("No pending order found!");
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to find a shop!");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpGet("/orders/{orderId}")]
        public IActionResult GetChosenOrder([FromRoute] int orderId)
        {
            Order order = _context.Order.Find(orderId);
            if (order != null)
            {
                var res = new JsonResult(order);
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to get this order!");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpPost("/orders/{orderId}")]
        public IActionResult SetChosenOrder([FromRoute] int orderId)
        {
            bool set = false;
            Order order = _context.Order.Find(orderId);
            if (set)
            {
                var res = new JsonResult("Successfully set order status!");
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Order doesn't exist!");
                res.StatusCode = 404;
                return res;
            }
        }
    }
}