using Microsoft.AspNetCore.Mvc;
using ShopModule.Data;
using ShopModule.Models;
using ShopModule.Products;
using ShopModule.Services;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
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

        /// <summary>
        /// Get products in a paginated fashion
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProductsEndpoint([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _service.GetPaginatedProductList(page, pageSize);
            return ResponseMessage.Success(result, 200);
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddProductsToShopEndpoint([FromBody] RequestProductMessage product)
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

        /// <summary>
        /// Remove product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public IActionResult DeleteProductEndpoint([FromRoute] Guid productId)
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

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public IActionResult GetProductInfoEndpoint([FromRoute] Guid productId)
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

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPut("{productId}")]
        public IActionResult UpdateProductInfoEndpoint([FromRoute] Guid productId, [FromBody] RequestProductMessage product)
        {
            ProductType category = _service.GetOrCreateCategory(product.category);
            var prod = _service.UpdateProduct(productId, product, category);
            if (prod != null)
            {
                return ResponseMessage.Success(prod.Convert(StaticData.defaultConverter), 200);
            }
            else
            {
                return ResponseMessage.Error("Product not found.", 404);
            }
        }

        /// <summary>
        /// Get products by category in a paginated fashion
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
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