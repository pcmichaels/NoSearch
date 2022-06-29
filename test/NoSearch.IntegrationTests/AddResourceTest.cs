using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NoSearch.App.Models;
using NoSearch.Data.DataAccess;
using System.Text.Json;
using Xunit;
using Microsoft.EntityFrameworkCore;

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
        public async Task BasicAdd_SubmitNew_Succeeds()
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

            var formEncoded = ConvertToFormData.ConvertToFormContent(submitNewViewModel);

            //var json = JsonSerializer.Serialize(submitNewViewModel);
            //var content = new StringContent(
            //    json, 
            //    System.Text.Encoding.UTF8,
            //    "application/json");

            // Act
            using var response = await httpClient.PostAsync(
                "/home/submitnew", formEncoded);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task BasicAdd_SubmitNew_AddRecord()
        {
            // Arrange
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                            typeof(DbContextOptions<NoSearchDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<NoSearchDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDB");
                        });
                    });
                });
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

            var formEncoded = ConvertToFormData.ConvertToFormContent(submitNewViewModel);
/*
            var json = JsonSerializer.Serialize(submitNewViewModel);
            var content = new StringContent(
                json,
                System.Text.Encoding.UTF8,
                "application/json");
*/

            // Act
            using var response = await httpClient.PostAsync(
                "/home/submitnew", formEncoded);

            // Assert
            Assert.True(response.IsSuccessStatusCode);            

            var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<NoSearchDbContext>();

            Assert.NotNull(dbContext);
            Assert.Single(dbContext!.Resources);
        }

    }
}
