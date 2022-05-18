using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [Route("clients")]
    [ApiController]
    public class ClientCotroller : ControllerBase
    {
        IHttpClientFactory _httpClientFactory;

        public ClientCotroller(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }
        [HttpGet]
        [Route("{clientAddress}")]
        public async Task<HttpResponseMessage> GetClientDetails([FromRoute] string clientAddress)
        {
            HttpResponseMessage response = null;
            using (var client = _httpClientFactory.CreateClient())
            {
                response = await client.GetAsync(GatewayOptions.ClientModulePath + "/clients/"+ clientAddress);
            }
            return response;
        }
    }
}
