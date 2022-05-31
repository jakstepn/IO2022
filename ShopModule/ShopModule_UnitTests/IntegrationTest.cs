using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopModule;
using ShopModule.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_UnitTests
{
    public class IntegrationTest<T> where T : DbContext
    {
        protected HttpClient testClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));

                        // Remove existing db context from the services
                        services.Remove(descriptor);

                        // Use in memory database
                        services.AddDbContext<ShopModuleDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemDbForTests");
                        });
                    });
            });
            testClient = appFactory.CreateClient();
        }
    }
}
