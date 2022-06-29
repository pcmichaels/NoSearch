using NoSearch.App.Models;
using NoSearch.IntegrationTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NoSearch.Tests.Utility
{
    public class ConvertToFormDataTests
    {
        [Fact]
        public async Task ConvertToFormContent_ClassConverted() 
        {
            // Arrange
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

            // Act
            var encodedContent = ConvertToFormData.ConvertToFormContent(submitNewViewModel);

            // Assert
            var result = await encodedContent.ReadAsFormDataAsync();
            Assert.Equal("IsValidated", result.AllKeys.Skip(1).First());
        }
    }
}
