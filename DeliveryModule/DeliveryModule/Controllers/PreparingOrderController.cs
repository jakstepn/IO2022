using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeliveryModule.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace DeliveryModule.Controllers
{
    [Route("orders")]
    [ApiController]
    public class PreparingOrderController : ControllerBase
    {
        private readonly DeliveryModuleDbContext _context;

        public PreparingOrderController(DeliveryModuleDbContext context)
        {
            _context = context;
        }

        public enum OrderStatusBody { Pending, InPreparation, ReadyForDelivery, PickedUpByCourier, RejectedByShop, RejectedByCustomer, Delivered };
        
        [HttpPut("{orderId}/InPreparation")]
        public async Task<IActionResult> TakeForPreparation([FromRoute] int orderId, [FromBody] OrderStatusBody orderStatus )
        {
            if (orderStatus == OrderStatusBody.InPreparation)
            {
                Order order = _context.Orders.Find(orderId);
                
                if (order == null)
                {
                    var res = new JsonResult("");
                    res.StatusCode = 404;
                    return res;
                }
                else
                {
                    order.SetOrderStatus(OrderStatusClass.OrderStatusEnum.InPreparation);
                    _context.SaveChanges();
                    return new JsonResult(orderStatus);
                }
            }
            return new JsonResult(NotFound());
        }
        [HttpPut("{orderId}/ReadyToPickUp")]
        public async Task<IActionResult> ReadyToPickUp([FromRoute] int orderId, [FromBody] OrderStatusBody orderStatus)
        {
            if (orderStatus == OrderStatusBody.ReadyForDelivery)
            {
                Order order = _context.Orders.Find(orderId);

                if (order == null)
                {
                    var res = new JsonResult("");
                    res.StatusCode = 404;
                    return res;
                }
                else
                {
                    order.SetOrderStatus(OrderStatusClass.OrderStatusEnum.ReadyToPickUp);
                    _context.SaveChanges();                    
                    return new JsonResult(orderStatus);
                }
            }
            return new JsonResult(NotFound(""));
        }
        [HttpPost]
        public IActionResult ComfirmOrderPickUp(int orderId)
        {
            Order order = _context.Orders.Find(orderId);

            if (order == null)
            {
                var res = new JsonResult("");
                res.StatusCode = 404;
                return res;
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Routs.ShopApi);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync($"/orders/{orderId}/confirm", OrderStatusBody.PickedUpByCourier);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
        [HttpPut("{orderId}/RejectOrder")]
        public IActionResult ShopRejectedOrder([FromRoute] int orderId, [FromBody] OrderStatusBody orderStatus)
        {
            if (orderStatus == OrderStatusBody.RejectedByShop)
            {
                Order order = _context.Orders.Find(orderId);

                if (order == null)
                {
                    var res = new JsonResult("");
                    res.StatusCode = 404;
                    return res;
                }
                else
                {
                    order.SetOrderStatus(OrderStatusClass.OrderStatusEnum.Rejected);
                    _context.SaveChanges();
                    return new JsonResult(orderStatus);
                }
            }
            return new JsonResult(NotFound(""));
        }
    }
}
