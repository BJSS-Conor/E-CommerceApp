using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsAPI.Contracts.Responses.Categories;
using ProductsAPI.Controllers;
using ProductsAPI.Models;
using ProductsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.UnitTest.Controllers
{    
    internal class UT_CategoryController
    {
        private Mock<ICategoryService> _mockCategoryService;
        private CategoryController _categoryController;

        [SetUp]
        public void Setup()
        {
            _mockCategoryService = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_mockCategoryService.Object);
        }

        [Test]
        public async Task GetAllCategories_Test()
        {
            var categories = new List<Category>()
            {
                new Category(1, "Category 1"),
                new Category(2, "Category 2"),
                new Category(3, "Category 3")
            };
            var mockResponse = new GetAllCategoriesResponse(true, categories);
            _mockCategoryService.Setup(service => service.GetAllCategories()).ReturnsAsync(mockResponse);

            var expectedStatusCode = 200;

            var result = await _categoryController.GetAllCategories() as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            // SHOULDN'T DO IT LIKE THIS, PRODUCT SERVICE DOES IT BETTER WAY, CANT BE BOTHERED TO CHANGE EVERYTHING
            var res = result.Value as GetAllCategoriesResponse;
            Assert.That(res.Categories, Is.EqualTo(categories));
        }

        [Test]
        public async Task GetCategoryById_ValidId()
        {
            var id = 1;
            var category = new Category(id, "Category 1");
            var mockResponse = new GetCategoryByIdResponse(true, category, null);
            _mockCategoryService.Setup(service => service.GetCategoryById(id)).ReturnsAsync(mockResponse);

            var expectedStatusCode = 200;

            var result = await _categoryController.GetCategoryById(id) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(category));            
        }

        [Test]
        public async Task GetCategoryById_InvalidId()
        {
            var id = 0;
            var expectedError = $"Category with ID {id} does not exist";
            var mockResponse = new GetCategoryByIdResponse(false, null, expectedError);
            _mockCategoryService.Setup(service => service.GetCategoryById(id)).ReturnsAsync(mockResponse);

            var expectedStatusCode = 404;

            var result = await _categoryController.GetCategoryById(id) as NotFoundObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(expectedError));
        }        

        [Test]
        public async Task UpdateCategory_ValidId()
        {
            var id = 1;
            var updatedName = "Updated Name";

            var category = new Category(id, "Category");
            var updatedCategory = new Category(id, updatedName);
            var expectedResCategory = new UpdateCategoryResponse(true, updatedCategory, null);
            _mockCategoryService.Setup(service => service.UpdateCategory(id, category)).ReturnsAsync(expectedResCategory);

            var expectedStatusCode = 200;

            var result = await _categoryController.UpdateCategory(id, category) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(updatedCategory));
        }

        [Test]
        public async Task UpdateCategory_InvalidId()
        {
            var id = 0;
            var updatedName = "Updated Name";
            var category = new Category(id, updatedName);

            var expectedError = $"Category with ID {id} does not exist";
            var expectedResCategory = new UpdateCategoryResponse(false, null, expectedError);
            _mockCategoryService.Setup(service => service.UpdateCategory(id, category)).ReturnsAsync(expectedResCategory);

            var expectedStatusCode = 404;

            var result = await _categoryController.UpdateCategory(id, category) as NotFoundObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task UpdateCategory_InvalidName()
        {
            var id = 0;
            var updatedName = "";
            var category = new Category(id, updatedName);

            var expectedStatusCode = 400;
            var expectedError = "Failed to update category: Name cannot be empty";            

            var result = await _categoryController.UpdateCategory(id, category) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task DeleteCategory_ValidId()
        {
            var id = 1;
            var category = new Category(id, "Category");

            var expectedStatusCode = 204;
            var expectedRes = new DeleteCategoryResponse(true, null);
            _mockCategoryService.Setup(service => service.DeleteCategory(id)).ReturnsAsync(expectedRes);

            var result = await _categoryController.DeleteCategory(id) as NoContentResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));            
        }

        [Test]
        public async Task DeleteCategory_InvalidId()
        {
            var id = 1;
            var category = new Category(id, "Category");

            var expectedStatusCode = 400;
            var expectedError = $"Category with ID {id} does not exist";
            var expectedRes = new DeleteCategoryResponse(false, expectedError);
            _mockCategoryService.Setup(service => service.DeleteCategory(id)).ReturnsAsync(expectedRes);

            var result = await _categoryController.DeleteCategory(id) as BadRequestObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(result.Value, Is.EqualTo(expectedError));
        }
    }
}
