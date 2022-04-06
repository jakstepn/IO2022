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
            _context.Couriers.Add(new Courier { CurrentOrder = null, PhoneNumber = "123213302" });
            _context.Orders.Add(new Order { Client = new Client { PhoneNumber = "123456789" }, IsPaid = false });

            _context.SaveChanges();
        }

        public enum OrderStatusBody { Pending, InPreparation, ReadyForDelivery, PickedUpByCourier, RejectedByShop, RejectedByCustomer, Delivered };

        [HttpPut("{orderId}/InPreparation")]
        public async Task<IActionResult> TakeForPreparation([FromRoute] int orderId, [FromBody] OrderStatusBody orderStatus)
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
                    order.SetOrderStatus(Order.OrderStatusEnum.InPreparation);
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
                    order.SetOrderStatus(Order.OrderStatusEnum.ReadyToPickUp);
                    _context.SaveChanges();
                    return new JsonResult(orderStatus);
                }
            }
            return new JsonResult(NotFound(""));
        }
        [HttpPost("/ComfirmOrder/{orderId}")]
        public IActionResult ComfirmOrderPickUp([FromRoute] int orderId)
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
        [HttpPut("{Id}/RejectOrder")]
        public IActionResult ShopRejectedOrder([FromRoute] string Id, [FromBody] OrderStatusBody orderStatus)
        {
            var res = new JsonResult("");
            Guid orderId;
            if (Id == null || !Guid.TryParse(Id, out orderId))
            {
                res.StatusCode = 404;
                return res;
            }
            if (orderStatus == OrderStatusBody.RejectedByShop)
            {
                Order order = _context.Orders.Find(orderId);

                if (order == null)
                {

                    res.StatusCode = 404;
                    return res;
                }
                else
                {
                    order.SetOrderStatus(Order.OrderStatusEnum.Rejected);
                    _context.SaveChanges();
                    return new JsonResult(orderStatus);
                }
            }
            return new JsonResult(NotFound(""));
        }
        [HttpPost("/requestPickup")]
        public IActionResult RequestPickup([FromBody] string orderId)
        {
            Guid gCourierId, gOrderId;
            if ( !Guid.TryParse(orderId, out gOrderId)) return new NotFoundResult();

            var order = _context.Orders.Find(gOrderId);

            if (order == null) return new NotFoundResult();

            order.SetOrderStatus(Order.OrderStatusEnum.ReadyToPickUp);

            /*var courier = _context.Couriers.Find(gCourierId);

            if(courier == null) return;

            courier.CurrentOrder = order;*/

            _context.SaveChanges();
            return Ok();
        }
    }
}
