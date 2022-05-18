using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
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
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostProduct()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> GetProductById()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> DeleteProductById()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<HttpResponseMessage> PutProductById()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpGet]
        [Route("category/{category}")]
        public async Task<HttpResponseMessage> GetProductsInCategory()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }
    }
}
