using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShopModule;
using ShopModule.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopModule_UnitTests.Integration
{
    public class CustomWebApplicationFactory<TStartup, Context> : WebApplicationFactory<TStartup> where TStartup : class where Context : DbContext
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<Context>));

                // Remove existing db context from the services
                services.Remove(descriptor);

                // Use in memory database
                services.AddDbContext<Context>(options =>
                {
                    options.UseInMemoryDatabase("InMemDbForTests");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scoped = scope.ServiceProvider;
                    var db = scoped.GetRequiredService<Context>();
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
