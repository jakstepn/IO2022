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
                     
        
        [HttpPost("/CourierRejectOrder")]
        public IActionResult CourierRejectOrder([FromBody] Guid courierId)
        {
            var owner = _context.Couriers.Find(courierId);

            if (owner == null)
            {
                return NotFound();
            }
            var destination = GetAvailableCourier();

            if (destination == null) return NotFound();
            _context.Entry(owner).Reference(c => c.CurrentOrder).Load();
            Shop.TransferOrderToAnotherCourier(owner, destination);
            _context.Entry(owner).Reference(c => c.CurrentOrder).CurrentValue = null;
            _context.SaveChanges();
            return Ok();
        }

        

        [HttpPost("/requestPickup")]
        public ActionResult RequestPickup([FromBody] Guid orderId )
        {
            var order = _context.Orders.Find(orderId);

            if (order == null)
            {
                order = GetOrder(orderId);
                if (order == null) return NotFound();
                _context.Orders.Add(order);
            }
            var courier = GetAvailableCourier();

            if (courier == null) return NotFound();
            courier.CurrentOrder = order;
            courier.CurrentState = Courier.CourierStatusEnum.DuringDelivery;
            _context.History.Add(new History(courier.Id,orderId));
            _context.SaveChanges();
            return Ok();
        }
        private Order GetOrder(Guid Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Routs.ShopApi);

                var getTask = client.GetAsync($"orders/{Id}").Result;
                if (!getTask.IsSuccessStatusCode)
                {
                    return null;
                }
                var ShopOrder = getTask.Content.ReadFromJsonAsync<ShopModule_ApiClasses.Messages.OrderMessage>().Result;
                return new Order(ShopOrder);
            }
        }
        
        private Courier GetAvailableCourier()
        {
            return _context.Couriers.Where(x => x.CurrentState == Courier.CourierStatusEnum.AvaibleForDelivery).First();
        }


    }
}
