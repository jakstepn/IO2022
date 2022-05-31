using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    public static class GatewayOptions
    {
        public static string ClientModulePath;
        public static string DeliveryModulePath;
        public static string ShopModulePath = "http://host.docker.internal:44385";
    }
    public class GatewayController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;
        public GatewayController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        
    }
}
