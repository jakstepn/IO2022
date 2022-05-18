using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

using ShopModule_ApiClasses.Messages;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ShopModule_ApiClasses.Structures;

namespace ApiGateway.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;
        public OrdersController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;   
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetClientDetails()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpGet]
        [Route("history")]
        public async Task<HttpResponseMessage> GetOrdersHistory()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/history");
            }
            return response;
        }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> CreateOrder([FromBody] OrderMessage message)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                string jsonString = JsonSerializer.Serialize(message);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                response = await client.PostAsync(GatewayOptions.ShopModulePath + "/orders/place", content);
            }
            return response;
        }

        [HttpGet]
        [Route("pending/{shopId}")]
        public async Task<HttpResponseMessage> GetPendingOrder([FromRoute] string shopId)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/shopId");
            }
            return response;
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<HttpResponseMessage> GetChosenOrder([FromRoute] string orderId)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ShopModulePath + "/orders/"+orderId);
            }
            return response;
        }
        [HttpPut]
        [Route("{orderId}")]
        public async Task<HttpResponseMessage> PutChosenOrder([FromRoute] string orderId, [FromBody] OrderStatus status)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                string jsonString = JsonSerializer.Serialize(status);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                response = await client.PutAsync(GatewayOptions.ShopModulePath + "/orders/"+orderId, content);
            }
            return response;
        }

        [HttpPut]
        [Route("{orderId}/payment")]
        public async Task<HttpResponseMessage> UpdatePaymentStatus([FromRoute] string orderId)
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
