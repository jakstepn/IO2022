using Microsoft.AspNetCore.Mvc;
using ShopModule_ApiClasses.Messages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("complaints")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;

        public ComplaintsController(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        [HttpPost]
        [Route("create")]
        public async Task<HttpResponseMessage> CreateComplaint([FromBody] ComplaintMessage complaint)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                string jsonString = JsonSerializer.Serialize(complaint);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                response = await client.PostAsync(GatewayOptions.ClientModulePath + "/orders/", content);
            }
            return response;
        }

        [HttpGet]
        [Route("pending/{shopId}")]
        public async Task<HttpResponseMessage> GetPendingComplaints()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpGet]
        [Route("{complaintId}")]
        public async Task<HttpResponseMessage> GetChosenComplaint()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpPut]
        [Route("{complaintId}/accept")]
        public async Task<HttpResponseMessage> AcceptComoplaint()
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/orders/");
            }
            return response;
        }

        [HttpPut]
        [Route("{complaintId}/reject")]
        public async Task<HttpResponseMessage> RejectComplaint()
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
