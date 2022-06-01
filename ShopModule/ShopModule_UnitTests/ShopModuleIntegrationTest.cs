using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopModule.Converters;
using ShopModule.Data;
using ShopModule.Orders;
using ShopModule.Products;
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
            var prodPost = await createProduct(client);
            Assert.Equal(HttpStatusCode.OK, prodPost.StatusCode);
            var createdPost = await CreateOrder(guid, client);
            Assert.Equal(HttpStatusCode.OK, createdPost.StatusCode);
            //var response = await GetOrder(guid, client);
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task<HttpResponseMessage> createProduct(HttpClient c)
        {
            ProductMessage p = new ProductMessage
            {
                quantity = 1000,
                category = "test",
                name = "testname",
                price = 10,
            };

            return await c.PostAsJsonAsync("http://localhost/products", p);
        }

        private async Task<HttpResponseMessage> CreateOrder(Guid oguid, HttpClient c)
        {
            var itId = Guid.NewGuid();
            OrderItemMessage orderItem = new OrderItemMessage
            {
                currency = "USD",
                grossPrice = 10,
                orderItemId = itId,
                productName = "testname",
                quantity = 1,
            };
            OrderMessage om = new OrderMessage {
                additionalInfo = "additionalInformation",
                clientAddress = null,
                confirmedPayment = false,
                creationDate = DateTime.Now,
                deliveryDate = DateTime.MinValue,
                orderId = oguid,
                orderItems = new OrderItemMessage[] { orderItem },
                orderStatus = "Pending",
            };
            return await c.PostAsJsonAsync("http://localhost/api/orders/place", om);
        }
        private async Task<HttpResponseMessage> GetOrder(Guid oguid, HttpClient c)
        {
            return await c.GetAsync(String.Format("/api/orders/{0}", oguid));
        }
    }
}
