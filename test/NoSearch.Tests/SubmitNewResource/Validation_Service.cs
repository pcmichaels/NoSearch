using Moq;
using NoSearch.App.Resources;
using NoSearch.Data;
using NoSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NoSearch.Tests.SubmitNewResource
{
    public class Validation_Service
    {
        [Theory]
        [InlineData("test badword1", "no bad words here", false)]
        [InlineData("test BadWord1", "still no bad words", false)]
        [InlineData("No Bad words", "badword2", false)]
        [InlineData("No Bad words", "BadWord2", false)]
        [InlineData("No Bad words", "some text badWord2 more text", false)]
        [InlineData("No Bad words", "no bad words", true)]
        [InlineData("", "", true)]
        public void Validation_BadLanguage_ValidlyRejects(string name, string description, bool shouldSucceed)
        {
            // Arrange
            var restrictedWordsDataAccess = new Mock<IRestrictedWordsDataAccess>();
            restrictedWordsDataAccess.Setup(a => a.GetAll()).Returns(
                new[] { "badword1", "BadWord2" });
            var validationService = new ValidationService(restrictedWordsDataAccess.Object);
            var resource = new Resource()
            {
                Name = name,
                Description = description
            };

            // Act
            var result = validationService.ValidateResource(resource);

            // Assert
            Assert.Equal(shouldSucceed, result.IsSuccess);
        }
    }
}
