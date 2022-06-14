﻿using DeliveryModule.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DeliveryModule.Controllers
{
    public class InformationControler : Controller
    {
        private readonly DeliveryModuleDbContext _context;

        public InformationControler(DeliveryModuleDbContext context)
        {
            _context = context;
        }
        [HttpGet("AddTestData")]
        public IActionResult AddTestData()
        {
            _context.Couriers.Add(new Courier { CurrentOrder = null, PhoneNumber = "123213302" });
            _context.Orders.Add(new Order { Client = new Client { PhoneNumber = "123456789" }, IsPaid = false });
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("status/{courierId}")]
        public IActionResult CourierStatus([FromRoute] Guid courierId)
        {    
            
            var res = new JsonResult("");
            var courier = _context.Couriers.Find(courierId);
            if (courier == null)
            {
                res.StatusCode = 404;
                res.Value = "Courier doen't exist";
                return res;
            }
            else
            {
                res.StatusCode = 200;
                res.Value = courier.CurrentState.ToString();
                return res;
            }
        }
        [HttpPatch("status/{courierId}")]
        public IActionResult PatchCourierStatus([FromRoute] Guid courierId,[FromBody] string orderStatus)
        {

            var res = new JsonResult("");
            var courier = _context.Couriers.Find(courierId);
            if (courier == null)
            {
                res.StatusCode = 404;
                res.Value = "Courier doen't exist";
                return res;
            }
            else
            {
                Courier.CourierStatusEnum ParsedStatus;

                if(Enum.TryParse<Courier.CourierStatusEnum>(orderStatus,out ParsedStatus))
                {
                    res.StatusCode = 200;
                    courier.CurrentState = ParsedStatus;
                    res.Value = courier.CurrentState.ToString();
                    _context.SaveChanges();
                }
                else
                {
                    res.StatusCode = 404;
                    res.Value = "Courier doen't exist";
                    
                }
                return res;
            }
        }
        [HttpGet("/orders/{courierid}/current")]
        public IActionResult PatchCourierStatus([FromRoute] Guid courierid)
        {

            var res = new JsonResult("");
            var courier = _context.Couriers.Find(courierid);
            if (courier == null)
            {
                res.StatusCode = 404;
                res.Value = "Courier doen't exist";
                return res;
            }
            else
            {
                _context.Entry(courier).Reference(c => c.CurrentOrder).Load();
                return new JsonResult(courier.CurrentOrder);
            }
        }

        private ShopModule_ApiClasses.Messages.OrderMessage GetOrderMessage(Guid Id)
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
                return ShopOrder;
            }
        }
    }
}
