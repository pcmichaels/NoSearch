using Moq;
using NoSearch.App.Search;
using NoSearch.Data.Resources;
using NoSearch.Models;
using Xunit;

namespace NoSearch.Tests.Search
{
    public class SearchTests
    {
        [Fact]
        public void SearchResources_CorrectResultsReturned()
        {
            // Arrange
            var resourceDataAccess = new Mock<IResourceDataAccess>();
            resourceDataAccess.Setup(a => a.GetAllResources()).Returns(
                new List<ResourceModel>()
                {
                    new ResourceModel() { Name = "abc", Description = "abc" },
                    new ResourceModel() { Name = "test", Description = "abc" },
                    new ResourceModel() { Name = "abc", Description = "testing" },
                    new ResourceModel() { Name = "aaa", Description = "bbb" }
                });
            var searchService = new SearchService(resourceDataAccess.Object);

            // Act
            var result = searchService.SearchResources("test", false);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, a => a.Name == "abc");
        }
    }
}