using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Products;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using System;

namespace ShopModule.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllProductsEndpoint([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _service.GetPaginatedProductList(page, pageSize);
            return ResponseMessage.Success(result, 200);
        }
        [HttpPost]
        public IActionResult AddProductsToShopEndpoint([FromBody] ProductMessage product)
        {
            ProductType category = _service.GetOrCreateCategory(product.category);
            var result = _service.AddProduct(new Product(product, category));
            if (result != null)
            {
                return ResponseMessage.Success("Successfully added product.", 200);
            }
            else
            {
                return ResponseMessage.Error("Failed to add product", 404);
            }
        }
        [HttpDelete("{productId}")]
        public IActionResult DeleteProductEndpoint([FromRoute] string productId)
        {
            var prod = _service.RemoveProduct(productId);
            if (prod != null)
            {
                return ResponseMessage.Success(prod.Convert(StaticData.defaultConverter), 200);
            }
            else
            {
                return ResponseMessage.Error("Product not found", 404);
            }
        }
        [HttpGet("{productId}")]
        public IActionResult GetProductInfoEndpoint([FromRoute] string productId)
        {
            var prod = _service.FindProduct(productId);
            if (prod != null)
            {
                return ResponseMessage.Success(prod.Convert(StaticData.defaultConverter), 200);
            }
            else
            {
                return ResponseMessage.Error("Product not found.", 404);
            }
        }
        [HttpGet("category/{category}")]
        public IActionResult GetProductsFromCategoryEndpoint([FromRoute] string category,
            [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _service.GetPaginatedProductListFromCategory(page, pageSize, category);
            if (result != null)
            {
                return ResponseMessage.Success(result, 200);
            }
            else
            {
                return ResponseMessage.Error("Category not found.", 404);
            }
        }
    }
}