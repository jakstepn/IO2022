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

namespace ShopModule_UnitTests.Integration
{
    public class ShopModuleProductIntegrationTest : IClassFixture<CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext>>
    {
        private readonly CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> _factory;
        private readonly HttpClient _client;
        public ShopModuleProductIntegrationTest(CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateProductTest()
        {
            var prodPost = await createProduct("testname", "testcategory", _client);
            Assert.Equal(HttpStatusCode.OK, prodPost.StatusCode);
        }

        [Fact]
        public async Task GetSingleProductTest()
        {
            await createProduct("test1", "cat1", _client);
            var res = await _client.GetAsync("http://localhost/products/test1");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task GetProductsTest()
        {
            await createProduct("test1", "cat1", _client);
            await createProduct("test2", "cat1", _client);
            await createProduct("test3", "cat2", _client);

            var res = await _client.GetAsync("http://localhost/products?page=0&pageSize=0");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

        [Fact]
        public async Task DeleteProductTest()
        {
            await createProduct("test4", "cat2", _client);
            var res = await _client.DeleteAsync("http://localhost/products/test4");
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
            var res2 = await _client.GetAsync("http://localhost/products/test4");
            Assert.Equal(HttpStatusCode.NotFound, res2.StatusCode);
        }

        public static async Task<HttpResponseMessage> createProduct(string name, string category, HttpClient c)
        {
            ProductMessage p = new ProductMessage
            {
                quantity = 1000,
                category = category,
                name = name,
                price = 10,
            };

            return await c.PostAsJsonAsync("http://localhost/products", p);
        }
    }
}
