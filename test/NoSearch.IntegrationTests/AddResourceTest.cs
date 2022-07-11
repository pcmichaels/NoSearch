using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoSearch.App.Models;
using NoSearch.Data.DataAccess;
using Xunit;

namespace NoSearch.IntegrationTests
{
    public class AddResourceTest
    {
        [Fact]
        public async Task HomeController_SubmitNew_Get_OKStatus()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory<Program>();
            var httpClient = appFactory.CreateClient();

            // Act
            using var response = await httpClient.GetAsync("/home/submitnew");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task HomeController_SubmitNew_Post_OKStatus()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory<Program>();
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

            var dict = ConvertToDictionaryData.ConvertToFormContent(submitNewViewModel);
            var formEncoded = new FormUrlEncodedContent(dict);

            // Act
            using var response = await httpClient.PostAsync(
                "/home/submitnew", formEncoded);

            // Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task HomeController_SubmitNew_Post_AddRecord()
        {
            // Arrange
            var appFactory = new TestWebApplicationFactory<Program>();
            var scope = appFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<NoSearchDbContext>();

            var httpClient = appFactory.CreateClient();

            int initialResourceCount = dbContext?.Resources?.Count() ?? 0;

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

            var dict = ConvertToDictionaryData.ConvertToFormContent(submitNewViewModel);
            var urlEncodedContent = new FormUrlEncodedContent(dict);

            // Act
            using var response = await httpClient.PostAsync(
                "/home/submitnew", urlEncodedContent);

            // Assert
            Assert.True(response.IsSuccessStatusCode);

            Assert.NotNull(dbContext);
            Assert.Equal(initialResourceCount + 1, dbContext!.Resources.Count());
        }
    }
}
