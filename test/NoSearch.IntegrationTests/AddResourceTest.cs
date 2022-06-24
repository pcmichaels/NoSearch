using Microsoft.AspNetCore.Mvc.Testing;
using NoSearch.App.Models;
using System.Text.Json;
using Xunit;

namespace NoSearch.IntegrationTests
{
    public class AddResourceTest
    {
        [Fact]
        public async Task BasicAdd_SubmitNew_ReturnsPage()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Program>();
            var httpClient = appFactory.CreateClient();

            // Act
            using var response = await httpClient.GetAsync("/home/submitnew");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task BasicAdd_SubmitNew_AddsRecord()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Program>();
            var httpClient = appFactory.CreateClient();

            var submitNewViewModel = new SubmitNewViewModel()
            {
                IsValidated = true,
                NewResource = new Models.ResourceModel()
                {
                    Name = "test",
                    Uri = "www.test.com",
                    Description = "description"
                }
            };

            var json = JsonSerializer.Serialize(submitNewViewModel);
            var content = new StringContent(
                json, 
                System.Text.Encoding.UTF8,
                "application/json");

            // Act
            using var response = await httpClient.PostAsync(
                "/home/submitnew", content);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
