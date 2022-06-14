using ApiGateway_ApiClasses.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopModule_ApiClasses.Messages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetProduct()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/products");
            }
            return response;
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostProduct([FromBody]ProductMessage request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.PostAsync(GatewayOptions.ShopModulePath + "/products", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            }
            return response;
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> GetProductById([FromRoute] string productId)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/products/" + productId);
            }
            return response;
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> DeleteProductById([FromRoute] string productId)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/products/" + productId);
            }
            return response;
        }

        [HttpGet]
        [Route("category/{category}")]
        public async Task<HttpResponseMessage> GetProductsInCategory([FromRoute] string category,[FromQuery] PaginatedRequest request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/products/category/" + category);
            }
            return response;
        }
        [HttpGet]
        [Route("authorization/test")]
        [Authorize]
        public async Task<IActionResult> TestApiAuthorization(string category)
        {
            return new OkResult();
        }
    }
}
