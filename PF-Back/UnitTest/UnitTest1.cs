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

        private Mock<ICategoryRepository> mockForecast;

        [TestInitialize]
        public void Init()
        {
            mockForecast = new Mock<ICategoryRepository>();


            target = new CategoryController(
                new UnitOfWork(
                    new WebApplicationAPIContext(
                        new DbContextOptionsBuilder<WebApplicationAPIContext>()
                        .UseInMemoryDatabase("WebApplicationAPIContext")
                            .Options
                    )
                )
            );
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