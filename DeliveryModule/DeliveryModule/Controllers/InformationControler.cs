﻿using DeliveryModule.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
    }
}
