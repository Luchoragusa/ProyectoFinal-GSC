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
    public class UnitTest
    {
        private CategoryController target;
        private Mock<IUnitOfWork> mockUow;

        #region "Initialize"

        [TestInitialize]
        public void Init()
        {
            mockUow = new Mock<IUnitOfWork>();

            mockUow.Setup(x => x.CategoryRepository).Returns(Mock.Of<ICategoryRepository>());

            target = new CategoryController(mockUow.Object);
        }
        #endregion

        #region "Get All"

        [TestMethod]
        public void GetAll_Categories_Test_OkObjectResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetAll()).Returns(categories);
            
            // Act
            var actual = target.GetAll();

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(categories, ((OkObjectResult)actual).Value);
        }

        #endregion
        
        #region "Get One"

        [TestMethod]
        public void GetOne_Category_Test_OkObjectResult_validId()
        {
            // Arrange
            List<Category> categories = GetCategories();
            int id = 1;
            mockUow.Setup(x => x.CategoryRepository.GetById(id)).Returns(categories.FirstOrDefault(x => x.Id == id));

            // Act
            var actual = target.GetOne(id);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(categories.FirstOrDefault(x => x.Id == id), ((OkObjectResult)actual).Value);
        }

        [TestMethod]
        public void GetOne_Category_Test_OkObjectResult_invalidId()
        {
            // Arrange
            List<Category> categories = GetCategories();
            int id = 0;

            // Act
            var actual = target.GetOne(id);
            
            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetOne_Category_Test_NotFoundResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            int id = 100;
            mockUow.Setup(x => x.CategoryRepository.GetById(id)).Returns(categories.FirstOrDefault(x => x.Id == id));

            // Act
            var actual = target.GetOne(id);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        #endregion

        #region "Create"

        [TestMethod]
        public void Create_Category_Test_CreatedResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetByDescrpition(categories[1].Description)).Returns((Category)null);
            mockUow.Setup(x => x.CategoryRepository.Insert(categories[1])).Returns(categories[1]);

            // Act
            var actual = target.Create(categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(CreatedResult));
            Assert.AreEqual(categories[1], ((CreatedResult)actual).Value);
        }

        [TestMethod]
        public void Create_Category_Test_BodyNull()
        {
            // Arrange
            List<Category> categories = GetCategories();

            // Act
            var actual = target.Create(null);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Create_Category_Test_BodyInvalid()
        {
            // Arrange
            Category category = new Category()
            {
                Id = 1,
                Description = null
            };

            // Act
            var actual = target.Create(category);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }
        
        [TestMethod]
        public void Create_Category_Test_AlreadyExist()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetByDescrpition(categories[1].Description)).Returns(categories[1]);

            // Act
            var actual = target.Create(categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Create_Category_Test_NonCreated()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetByDescrpition(categories[1].Description)).Returns((Category)null);
            mockUow.Setup(x => x.CategoryRepository.Insert(categories[1])).Returns((Category)null);

            // Act
            var actual = target.Create(categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        #endregion

        #region "Update"

        [TestMethod]
        public void Update_Category_Test_OkObjectResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetById(categories[1].Id)).Returns(categories[1]);
            mockUow.Setup(x => x.CategoryRepository.Update(categories[1])).Returns(categories[1]);

            // Act
            var actual = target.Update(categories[1].Id, categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkObjectResult));
            Assert.AreEqual(categories[1], ((OkObjectResult)actual).Value);
        }

        [TestMethod]
        public void Update_Category_Test_BodyNull()
        {
            // Arrange
            List<Category> categories = GetCategories();

            // Act
            var actual = target.Update(categories[1].Id, null);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Update_Category_Test_idInvalid()
        {
            // Arrange
            List<Category> categories = GetCategories();
            int id = 0;
            // Act
            var actual = target.Update(id, categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Update_Category_Test_BodyInvalid()
        {
            // Arrange
            List<Category> categories = GetCategories();
            Category category = new Category()
            {
                Id = 1,
                Description = null
            };

            // Act
            var actual = target.Update(categories[1].Id, category);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Update_Category_Test_NotFoundResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetById(categories[1].Id)).Returns((Category)null);

            // Act
            var actual = target.Update(categories[1].Id, categories[1]);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        #endregion

        #region "Delete"

        [TestMethod]
        public void Delete_Category_Test_OkResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetById(categories[1].Id)).Returns(categories[1]);
            mockUow.Setup(x => x.CategoryRepository.Delete(categories[1].Id)).Returns(true);

            // Act
            var actual = target.Delete(categories[1].Id);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(OkResult));
        }

        [TestMethod]
        public void Delete_Category_Test_idInvalid()
        {
            // Arrange
            List<Category> categories = GetCategories();
            int id = 0;

            // Act
            var actual = target.Delete(id);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void Delete_Category_Test_NotFoundResult()
        {
            // Arrange
            List<Category> categories = GetCategories();
            mockUow.Setup(x => x.CategoryRepository.GetById(categories[1].Id)).Returns((Category)null);

            // Act
            var actual = target.Delete(categories[1].Id);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        #endregion

        public List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category() { Id = 1, Description = "Category 1" },
                new Category() { Id = 2, Description = "Category 2" },
                new Category() { Id = 3, Description = "Category 3" },
                new Category() { Id = 4, Description = "Category 4" },
                new Category() { Id = 5, Description = "Category 5" },
            };
        }
    }
}