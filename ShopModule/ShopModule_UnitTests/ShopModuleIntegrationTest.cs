using Microsoft.AspNetCore.Mvc;
using ShopModule.Converters;
using ShopModule.Data;
using ShopModule.Orders;
using ShopModule_ApiClasses.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class ShopModuleIntegrationTest : IntegrationTest<ShopModuleDbContext>
    {
        [Fact]
        public async Task CreateOrderTest()
        {
            Console.WriteLine("Start");
            Guid guid = Guid.NewGuid();
            Console.WriteLine("Testing");
            var createdPost = await CreateOrder(guid);
            Console.WriteLine(createdPost.orderId);
            var response = await GetOrder(guid);
            Console.WriteLine(response.orderId);
            Assert.Equal(createdPost.orderId, response.orderId);
        }

        private async Task<OrderMessage> CreateOrder(Guid oguid)
        {
            Order o = new Order
            {
                Id = oguid,
                AdditionalInfo = "t",
                ConfirmedPayment = false,
                CreationDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                OrderStatus = "Pending"
            };
            var response = await testClient.PostAsJsonAsync("/api/v1/orders/place", o.Convert(new MessageConverter()));
            return await response.Content.ReadFromJsonAsync<OrderMessage>();
        }
        private async Task<OrderMessage> GetOrder(Guid oguid)
        {
            var response = await testClient.GetAsync(String.Format("/api/v1/orders/{0}", oguid));
            return await response.Content.ReadFromJsonAsync<OrderMessage>();
        }
    }
}
