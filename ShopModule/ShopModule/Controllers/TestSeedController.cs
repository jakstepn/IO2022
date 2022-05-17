using Microsoft.AspNetCore.Mvc;
using ShopModule.Converters;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Orders;
using ShopModule.Products;
using ShopModule_ApiClasses.Messages;
using System.Collections.Generic;

namespace ShopModule.Controllers
{
    [Route("seed")]
    [ApiController]
    public class TestSeedController : Controller
    {
        private readonly ShopModuleDbContext _context;

        public TestSeedController(ShopModuleDbContext context)
        {
            _context = context;
        }
        [HttpPost("order")]
        public IActionResult AddOrder([FromBody] OrderMessage message)
        {
            Order o = new Order(message);
            foreach (var item in message.orderItems)
            {
                var found = _context.Products.Find(item.productName);
                if (found != null)
                {
                    _context.OrderItems.Add(new OrderItem(item, o, found));
                }
            }
            var pending = _context.Orders.Add(o);
            _context.SaveChanges();

            OrderMessage returnMessage = o.Convert(new MessageConverter());
            if (pending != null)
            {
                return ResponseMessage.Success(returnMessage, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending orders", 404);
            }
        }

    }
}
