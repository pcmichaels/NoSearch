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
        /*
        [Fact]
        public void ConvertToFormContent_ClassConverted() 
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
            var dict = ConvertToDictionaryData.ConvertToFormContent(submitNewViewModel);

            // Assert            
            Assert.True(Convert.ToBoolean(dict["IsValidated"]));
            Assert.Equal("test", dict["NewResource.Name"]);
        }
        */
    }
}
