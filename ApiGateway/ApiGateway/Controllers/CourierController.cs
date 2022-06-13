using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using ClientModule_ApiClasses.ClientModule;
using System;

namespace ApiGateway.Controllers
{
    [Route("courier")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        public static string courierId = "templateID";

        IHttpClientFactory _httpClientFactory;

        public CourierController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        [HttpGet]
        [Route("status")]
        public async Task<HttpResponseMessage> GetCourierStatus()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.DeliveryModulePath + "/status/"+ courierId);
            }
            return response;
        }

        [HttpPatch]
        [Route("status")]
        public async Task<HttpResponseMessage> UpdateCourierStatus([FromBody] string orderStatus)
        {
            throw new NotImplementedException("This method does not have its destination in documentation");

            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.DeliveryModulePath + "/status/" + courierId);
            }
            return response;
        }

    }
}
