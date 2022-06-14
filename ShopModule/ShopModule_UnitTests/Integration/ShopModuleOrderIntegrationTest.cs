using ShopModule.Data;
using ShopModule_ApiClasses.Messages;
using ShopModule_ApiClasses.Messages.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests.Integration
{
    public class ShopModuleOrderIntegrationTest : IClassFixture<CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext>>
    {
        private readonly CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> _factory;
        private readonly HttpClient _client;
        public ShopModuleOrderIntegrationTest(CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateOrderTest()
        {
            Guid guid = Guid.NewGuid();
            var createdPost = await CreateOrder(guid, "tesname", "testcat", _client);
            Assert.Equal(HttpStatusCode.Created, createdPost.StatusCode);
        }

        [Fact]
        public async Task ChangeOrderStatusTest()
        {
            Guid guid = Guid.NewGuid();
             await CreateOrder(guid, "tesname1", "testcat1", _client);
            var changed = await _client.PutAsJsonAsync("http://localhost/orders/" + guid.ToString(), "RejectedByShop");
            Assert.Equal(HttpStatusCode.OK, changed.StatusCode);
        }

        [Fact]
        public async Task ChangeOrderStatusAndNotifyDeliveryTest()
        {
            Guid guid = Guid.NewGuid();
            await CreateOrder(guid, "tesname2", "testcat2", _client);
            var changed = await _client.PutAsJsonAsync("http://localhost/orders/" + guid.ToString(), "ReadyForDelivery");
            Assert.Equal(HttpStatusCode.OK, changed.StatusCode);
        }

        [Fact]
        public async Task GetOrderTest()
        {
            Guid guid = Guid.NewGuid();
            await CreateOrder(guid, "tesname3", "testcat3", _client);
            var found = await _client.GetAsync("http://localhost/orders/" + guid.ToString());
            Assert.Equal(HttpStatusCode.OK, found.StatusCode);
        }

        [Fact]
        public async Task GetAllOrdersTest()
        {
            Guid guid = Guid.NewGuid();
            await CreateOrder(guid, "tesname4", "testcat4", _client);
            await CreateOrder(guid, "tesname5", "testcat5", _client);
            await CreateOrder(guid, "tesname6", "testcat6", _client);
            var found = await _client.GetAsync("http://localhost/orders/pending/0?page=0&pageSize=3");
            Assert.Equal(HttpStatusCode.OK, found.StatusCode);
        }

        public static async Task<HttpResponseMessage> CreateOrder(Guid oguid, string prodName, string catName, HttpClient c)
        {
            await ShopModuleProductIntegrationTest.createProduct(prodName, catName, c);
            var itId = Guid.NewGuid();
            RequestOrderItemMessage orderItem = new RequestOrderItemMessage
            {
                productId = itId,
                quantity = 1,
            };
            AddressMessage address = new AddressMessage
            {
                city = "nocity",
                street = "nostreet",
                zipCode = "nozip",
            };
            RequestOrderMessage om = new RequestOrderMessage
            {
                additionalInfo = "additionalInformation",
                clientAddress = address,
                orderItems = new RequestOrderItemMessage[] { orderItem },
            };
            return await c.PostAsJsonAsync("http://localhost/orders", om);
        }
    }
}
