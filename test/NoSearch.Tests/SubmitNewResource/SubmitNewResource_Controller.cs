using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NoSearch.App.Controllers;
using NoSearch.App.Models;
using NoSearch.App.Resources;
using NoSearch.App.Search;
using NoSearch.Common;
using NoSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NoSearch.Tests.SubmitNewResource
{
    public class SubmitNewResource_Controller
    {
        [Fact]
        public async Task Lookup_ReturnsNewVM_TagsPopulated()
        {
            // Arrange
            var logger = new Mock<ILogger<HomeController>>();
            var searchService = new Mock<ISearchService>();
            var resourceService = new Mock<IResourceService>();            
            resourceService.Setup(a => a.FindResource(It.IsAny<ResourceModel>())).ReturnsAsync(
                DataResult<ResourceModel>.Success(new ResourceModel() { Name = "Test" }));
            var validationService = new Mock<IValidationService>();

            var controller = new HomeController(logger.Object, 
                searchService.Object, resourceService.Object, validationService.Object);

            var submitNewViewModel = new SubmitNewViewModel()
            {
                AllTags = new[] {"Blogs", "Programming"}                
            };

            // Act
            var actionResult = await controller.Lookup(submitNewViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var resultModel = Assert.IsType<SubmitNewViewModel>(viewResult.ViewData.Model);

            Assert.Equal(2, resultModel.AllTags.Count());
            Assert.Equal("Test", resultModel.NewResource.Name);
        }

        [Fact]
        public async Task Lookup_NullTags_ReturnsNewVM_TagsPopulated()
        {
            // Arrange
            var logger = new Mock<ILogger<HomeController>>();
            var searchService = new Mock<ISearchService>();
            var resourceService = new Mock<IResourceService>();
            resourceService.Setup(a => a.FindResource(It.IsAny<ResourceModel>())).ReturnsAsync(
                DataResult<ResourceModel>.Success(new ResourceModel() { Name = "Test" }));
            resourceService.Setup(a => a.GetAllTags()).ReturnsAsync(
                DataResult<IEnumerable<TagModel>>.Success(new List<TagModel>() 
                { 
                    new TagModel("Tag1"),
                    new TagModel("Tag2"),
                }));
            var validationService = new Mock<IValidationService>();

            var controller = new HomeController(logger.Object,
                searchService.Object, resourceService.Object,
                validationService.Object);

            var submitNewViewModel = new SubmitNewViewModel()
            {
                AllTags = null
            };

            // Act
            var actionResult = await controller.Lookup(submitNewViewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var resultModel = Assert.IsType<SubmitNewViewModel>(viewResult.ViewData.Model);

            Assert.Equal(2, resultModel.AllTags.Count());
            Assert.Equal("Test", resultModel.NewResource.Name);
        }
    }
}
