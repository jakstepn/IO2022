using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Models;
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

            _context.ProductTypes.Add(new ProductType { Name = "test" });

            _context.SaveChanges();
        }

        [HttpPost("product")]
        public IActionResult AddProduct([FromBody] ProductMessage message)
        {
            var pending = _context.Products.Add(new Product(message));
            if (pending != null)
            {
                return ResponseMessage.Success(pending, 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to get pending orders", 404);
            }
        }
        [HttpGet("product_type")]
        public IActionResult AddProductType()
        {
            //var pending = _context.ProductTypes.Add(new ProductType(message));
            //if (pending != null)
            //{
            //    return ResponseMessage.Success(pending, 200);
            //}
            //else
            //{
            //    return ResponseMessage.Error("Failed to get pending orders", 404);
            //}


            var res = new JsonResult("");
            res.StatusCode = 200;
            res.Value = "test";
            return res;
        }
    }
}
