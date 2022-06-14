using ClientModule_ApiClasses.OrdersModule;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientModule_UnitTests.IntegrationTests
{
    public class ClientControllerIntegrationTest:IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            // Arrange
            //await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync("/orders");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<GetAllCurrentOrdersResponse>()).orderItems.Should().BeEmpty();
        }
    }
}
