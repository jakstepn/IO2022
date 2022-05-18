using ShopModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopModule_UnitTests
{
    public class DeliveryModuleIntegrationTest
    {

        private readonly HttpClient _client;

        public DeliveryModuleIntegrationTest()
        {
            _client = new HttpClient();
        }

        [Fact]
        public void RequestPickupTest()
        {

        }
    }
}
