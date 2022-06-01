using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopModule.Converters;
using ShopModule.Data;
using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class ShopModuleIntegrationTest : IClassFixture<CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext>>
    {
        private readonly CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> _factory;
        public ShopModuleIntegrationTest(CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateOrderTest()
        {
            var client = _factory.CreateClient();
            Guid guid = Guid.NewGuid();
            var createdPost = await CreateOrder(guid, client);
            //Assert.Equal(HttpStatusCode.OK, createdPost.StatusCode);
            var response = await GetOrder(guid, client);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<HttpResponseMessage> CreateOrder(Guid oguid, HttpClient c)
        {
            OrderMessage om = new OrderMessage { 
                additionalInfo = "additionalInformation",
                clientAddress = null,
                confirmedPayment = false,
                creationDate = DateTime.Now,
                deliveryDate = DateTime.MinValue,
                orderId = oguid,
                orderItems = null,
                orderStatus = "Pending",
            };
            string json = JsonConvert.SerializeObject(om);
            return await c.PostAsync("http://localhost/orders/place", new StringContent(json, Encoding.UTF8, "application/json"));
        }
        private async Task<HttpResponseMessage> GetOrder(Guid oguid, HttpClient c)
        {
            return await c.GetAsync(String.Format("/orders/{0}", oguid));
        }
    }
}
