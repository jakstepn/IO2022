using ShopModule.Data;
using ShopModule_ApiClasses.Messages;
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
    public class ShopModuleComplaintIntegrationTest : IClassFixture<CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext>>
    {
        private readonly CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> _factory;
        private readonly HttpClient _client;
        public ShopModuleComplaintIntegrationTest(CustomWebApplicationFactory<ShopModule.Startup, ShopModuleDbContext> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateComplaintTest()
        {
            Guid oid = Guid.NewGuid();
            Guid cid = Guid.NewGuid();
            var complaintPost = await createComplaint(cid, oid, _client);
            Assert.Equal(HttpStatusCode.Created, complaintPost.StatusCode);
        }

        [Fact]
        public async Task GetComplaintTest()
        {
            Guid oid = Guid.NewGuid();
            Guid cid = Guid.NewGuid();
            await createComplaint(cid, oid, _client);
            var complaintFind = await _client.GetAsync("http://localhost/complaints/" + cid.ToString());
            Assert.Equal(HttpStatusCode.OK, complaintFind.StatusCode);
        }

        [Fact]
        public async Task SetAcceptComplaintTest()
        {
            Guid oid = Guid.NewGuid();
            Guid cid = Guid.NewGuid();
            await createComplaint(cid, oid, _client);
            var complaintFind = await _client.PutAsJsonAsync("http://localhost/complaints/" + cid.ToString() + "/accept", "Accepted");
            Assert.Equal(HttpStatusCode.OK, complaintFind.StatusCode);
        }

        [Fact]
        public async Task SetRejectComplaintTest()
        {
            Guid oid = Guid.NewGuid();
            Guid cid = Guid.NewGuid();
            await createComplaint(cid, oid, _client);
            var complaintFind = await _client.PutAsJsonAsync("http://localhost/complaints/" + cid.ToString() + "/reject", "Rejected");
            Assert.Equal(HttpStatusCode.OK, complaintFind.StatusCode);
        }

        [Fact]
        public async Task GetAllComplaintsTest()
        {
            Guid oid = Guid.NewGuid();
            Guid oid1 = Guid.NewGuid();
            Guid oid2 = Guid.NewGuid();
            Guid cid = Guid.NewGuid();
            Guid cid1 = Guid.NewGuid();
            Guid cid2 = Guid.NewGuid();
            await createComplaint(cid, oid, _client);
            await createComplaint(cid1, oid1, _client);
            await createComplaint(cid2, oid2, _client);
            var complaintFind = await _client.GetAsync("http://localhost/complaints/pending/0?page=0&pageSize=3");
            Assert.Equal(HttpStatusCode.OK, complaintFind.StatusCode);
        }

        private static async Task<HttpResponseMessage> createComplaint(Guid cid, Guid orderid, HttpClient c)
        {
            ComplaintMessage com = new ComplaintMessage
            {
                complaintId = cid,
                orderId = orderid,
                status = "Pending",
                text = "Testcomplaint"
            };

            return await c.PostAsJsonAsync("http://localhost/complaints/create", com);
        }
    }
}
