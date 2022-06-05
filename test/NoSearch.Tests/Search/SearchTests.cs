using Moq;
using NoSearch.App.Search;
using NoSearch.Data;
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
                new List<Resource>()
                {
                    new Resource() { Name = "abc", Description = "abc" },
                    new Resource() { Name = "test", Description = "abc" },
                    new Resource() { Name = "abc", Description = "testing" },
                    new Resource() { Name = "aaa", Description = "bbb" }
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