using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using WebApplicationAPI.Controllers;
using WebApplicationAPI.DataAccess;
using WebApplicationAPI.DataAccess.CategoryF;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private CategoryController target;
        private Mock<IUnitOfWork> mockUow;
        
        [TestInitialize]
        public void Init()
        {
            mockUow = new Mock<IUnitOfWork>();

            mockUow.Setup(x => x.CategoryRepository).Returns(Mock.Of<ICategoryRepository>());
            // mockUow.Setup(x => x.CategoryRepository.GetAll()).Returns(new List<Category>());

            target = new CategoryController(mockUow.Object);
        }

        [TestMethod]
        public void GetAll_Categories_Test_ObjectResult()
        {

            // Arrange
            //var expected = new Category { Id = 1, Description = "Category 1" };
            //mockForecast.Setup(m => m.GetAll()).Returns(new List<Category> { expected });

            // Act
            var actual = target.GetAll();

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
        }
    }
}