using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Products;
namespace ShopModule.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly ShopModuleDbContext _context;

        public ProductController(ShopModuleDbContext context)
        {
            _context = context;
        }

        [HttpGet("/products")]
        public IActionResult GetAllProducts([FromQuery] int page, [FromRoute] int pageSize)
        {
            // TODO
            // Return paginated product list

            return new JsonResult("Success!");
        }
        [HttpGet("/products")]
        public IActionResult AddProductsToShop([FromBody] int name, [FromBody] int category,
            [FromBody] decimal price, [FromBody] int quantity)
        {
            // TODO
            // Add product to the database

            // TODO
            // Implement Product constructor
            Product prod = new Product();

            bool addedProduct = false;
            if (addedProduct)
            {
                var res = new JsonResult("Successfully added product!");
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Failed to add product!");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpDelete("/products/{productId}")]
        public IActionResult DeleteProduct([FromRoute] string productId)
        {
            var prod = _context.Products.Find(productId);
            if (prod != null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
                var res = new JsonResult(prod);
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Product not found.");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpGet("/products/{productId}")]
        public IActionResult GetProductInfo([FromRoute] string productId)
        {
            var prod = _context.Products.Find(productId);
            if (prod != null)
            {
                var res = new JsonResult(prod);
                res.StatusCode = 200;
                return res;
            }
            else
            {
                var res = new JsonResult("Product not found.");
                res.StatusCode = 404;
                return res;
            }
        }
        [HttpGet("/products/{category}")]
        public IActionResult GetProductsFromCategory([FromRoute] string category, [FromQuery] int page, [FromRoute] int pageSize)
        {
            // TODO
            // Return paginated product list from a category

            return new JsonResult("Success!");
        }
    }
}