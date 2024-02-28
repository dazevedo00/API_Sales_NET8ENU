using Microsoft.AspNetCore.Mvc.Testing;

namespace API_Sales_NET8.Tests
{


    [TestClass]
    public class SalesItemTests
    {
        private readonly HttpClient _client;

        public SalesItemTests()
        {
            var appFactory = new WebApplicationFactory<API_Sales_NET8.Startup>();
            _client = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task GetSalesItems_ReturnsOkResponse()
        {
            // Arrange - Assuming there are some sales items in the database
            // You may need to seed the database or use a specific test database

            // Act
            var response = await _client.GetAsync("/api/salesitems");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

        }

        // Todo Implement next tests
    }
}