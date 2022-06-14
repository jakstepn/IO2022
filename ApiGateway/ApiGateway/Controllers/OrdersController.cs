using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Structures;

using ApiGateway_ApiClasses.Requests;


using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using System;

namespace ApiGateway.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public static string shopId = "templateID";
        public static string clientId = "templateID";

        IHttpClientFactory _httpClientFactory;
        public OrdersController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;   
        }

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> CreateClientDetails([FromBody] OrderMessage request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.PostAsync(GatewayOptions.ShopModulePath + "/orders/place", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            }
            return response;
        }

        [HttpGet]
        [Route("shop")]
        public async Task<HttpResponseMessage> GetAllShopOrders([FromQuery] PaginatedRequest request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/"+shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("shop/pending")]
        public async Task<HttpResponseMessage> GetAllPendingShopOrders([FromQuery] PaginatedRequest request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("courier")]
        public async Task<HttpResponseMessage> GetAllCourierOrders([FromQuery] PaginatedRequest request)
        {
            throw new NotImplementedException("This method does not have its destination in documentation");

            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("courier/current")]
        public async Task<HttpResponseMessage> GetCurrentCourierOrder([FromQuery] PaginatedRequest request)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("customer")]
        public async Task<HttpResponseMessage> GetAllCustomerOrders([FromQuery] PaginatedRequest request)
        {

            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("customer/pending")]
        public async Task<HttpResponseMessage> GetAllPendingCustomerOrders([FromQuery] PaginatedRequest request)
        {
            throw new NotImplementedException("This method does not have its destination in documentation");

            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<HttpResponseMessage> GetSelectedOrder([FromRoute] string orderId)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/" + orderId);
            }
            return response;
        }

        [HttpPatch]
        [Route("{orderId}")]
        public async Task<HttpResponseMessage> UpdateOrderStatus([FromRoute] string orderId, [FromBody] string orderStatus)
        {
            throw new NotImplementedException("This method does not have its destination in documentation");

            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/pending/" + shopId);
            }
            return response;
        }
    }
}
