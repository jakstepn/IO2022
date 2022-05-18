using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    public static class GatewayOptions
    {
        public static string ClientModulePath;
        public static string DeliveryModulePath;
        public static string ShopModulePath;
    }
    public class GatewayController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;
        public GatewayController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        [HttpGet]
        [Route("orders")]
        public async Task<HttpResponseMessage> GetClientDetails()
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
