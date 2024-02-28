using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace API_Sales_NET8.Tests
{
    [TestClass]
    public class CustomerTests
    {
        private readonly HttpClient _client;

        public CustomerTests()
        {
            var appFactory = new WebApplicationFactory<API_Sales_NET8.Startup>();
            _client = appFactory.CreateClient();
        }

        [TestMethod]
        public async Task GetCustomers_ReturnsOkResponse()
        {
            // Arrange
            var requestUri = "/api/customers";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        /// <summary>
        /// Tests if creating a customer returns an HTTP 201 (Created) response.
        /// </summary>
        [TestMethod]
        public async Task CreateCustomer_ReturnsCreatedResponse()
        {
            // Arrange
            var customerId = "C999"; // Use the ID or another identification of the customer to be deleted

            // Act - Delete customer if exists
            await _client.DeleteAsync($"/api/customers/{customerId}");

            // Arrange - Create a new customer
            var newCustomer = new { Code = customerId, Name = "Test Customer", Country = "PT", TaxId = "123456789", CreatedAt = DateTime.Now };

            // Act - Create a new customer
            var response = await _client.PostAsync("/api/customers", ContentHelper.GetStringContent(newCustomer));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Tests if updating a customer returns an HTTP 200 (OK) response.
        /// </summary>
        [TestMethod]
        public async Task UpdateCustomer_ReturnsOkResponse()
        {
            // Arrange
            var customerId = "C999"; // Use the ID of an existing customer
            var requestUri = $"/api/customers/{customerId}";
            var updatedCustomer = new { Name = "Updated Customer", Country = "PT", TaxId = "123456789", UpdatedAt = DateTime.Now };

            // Act
            var response = await _client.PutAsync(requestUri, ContentHelper.GetStringContent(updatedCustomer));

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Tests if deleting a customer returns an HTTP 204 (No Content) response.
        /// </summary>
        [TestMethod]
        public async Task DeleteCustomer_ReturnsNoContentResponse()
        {
            var customerId = "D999";

            // Arrange
            var newCustomer = new { Code = customerId, Name = "Test Customer", Country = "USA", TaxId = "123456789", CreatedAt = DateTime.Now };
            var createResponse = await _client.PostAsync("/api/customers",ContentHelper.GetStringContent(newCustomer));
            createResponse.EnsureSuccessStatusCode();

            // Act
            var deleteResponse = await _client.DeleteAsync($"/api/customers/{customerId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}