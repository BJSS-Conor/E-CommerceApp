using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsAPI.Contracts.Responses.Products;
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
    internal class UT_ProductsController
    {
        private Mock<IProductCatalogueService> _mockProductService;
        private ProductsController _productsController;

        [SetUp]
        public void Setup()
        {
            _mockProductService = new Mock<IProductCatalogueService>();
            _productsController = new ProductsController(_mockProductService.Object);
        }

        [Test]
        public async Task GetAllProducts_Test()
        {
            var expectedStatusCode = 200;
            var products = new List<Product>()
            {
                new Product(1, "Product 1", "Description 1", 10, 1, 20),
                new Product(1, "Product 2", "Description 2", 10, 2, 20),
                new Product(1, "Product 3", "Description 3", 10, 1, 30),
            };
            _mockProductService.Setup(service => service.GetAllProducts()).ReturnsAsync(products);           

            var res = await _productsController.GetAllProducts() as OkObjectResult;

            Assert.IsNotNull(res);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(products));
        }

        [Test]
        public async Task GetProductById_ValidId()
        {
            var expectedStatusCode = 200;

            var id = 1;
            var product = new Product(id, "Test Product", "Test", 10, 2, 5);
            var expectedRes = new GetProductResponse(true, product, null);
            _mockProductService.Setup(service => service.GetProductById(id)).ReturnsAsync(expectedRes);

            var res = await _productsController.GetProductById(id) as OkObjectResult;

            Assert.IsNotNull(res);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(product));
        }

        [Test]
        public async Task GetProductById_InvalidId()
        {
            var id = 0;
            var expectedStatusCode = 404;
            var expectedError = $"Product with ID {id} does not exist";

            var expectedRes = new GetProductResponse(false, null, expectedError);

            _mockProductService.Setup(service => service.GetProductById(id)).ReturnsAsync(expectedRes);

            var res = await _productsController.GetProductById(id) as NotFoundObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
            
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public async Task CreateProduct_ValidProduct()
        {            
            var expectedStatusCode = 201;
            var product = new Product(1, "Test Product", "Description", 10, 2, 30);            

            _mockProductService.Setup(service => service.CreateProduct(product)).ReturnsAsync(product);

            var res = await _productsController.CreateProduct(product) as CreatedAtRouteResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(product));
        }

        [Test]
        public async Task CreateProduct_InvalidName()
        {
            var expectedStatusCode = 400;
            var expectedError = "Failed to create product: Name or Description cannot be null";
            var product = new Product(1, "", "description", 30, 2, 10);

            var res = await _productsController.CreateProduct(product) as BadRequestObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task CreateProduct_InvalidDescription()
        {
            var expectedStatusCode = 400;
            var expectedError = "Failed to create product: Name or Description cannot be null";
            var product = new Product(1, "Test Product", "", 30, 2, 10);

            var res = await _productsController.CreateProduct(product) as BadRequestObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task UpdateProduct_ValidId()
        {
            var expectedStatusCode = 200;
            var id = 1;
            var product = new Product(id, "Updated Product", "Updated Description", 10, 2, 10);
            var updatedProduct = new Product(id, "Product", "Description", 10, 2, 10);

            var expectedRes = new UpdateProductResponse(true, updatedProduct, null);
            _mockProductService.Setup(service => service.UpdateProduct(id, product)).ReturnsAsync(expectedRes);

            var res = await _productsController.UpdateProduct(id, product) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(updatedProduct));
        }

        [Test]
        public async Task UpdateProduct_InvalidId()
        {           
            var id = 0;
            var product = new Product(id, "Updated Product", "Updated Description", 10, 2, 10);
            var expectedStatusCode = 404;
            var expectedError = $"Product with ID {id} does not exist";

            var expectedRes = new UpdateProductResponse(false, null, expectedError);
            _mockProductService.Setup(service => service.UpdateProduct(id, product)).ReturnsAsync(expectedRes);

            var res = await _productsController.UpdateProduct(id, product) as NotFoundObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task UpdateProduct_InvalidName()
        {
            var id = 0;
            var product = new Product(id, "", "Updated Description", 10, 2, 10);
            var expectedStatusCode = 400;
            var expectedError = "Failed to update product: Name or description cannot be empty";            

            var res = await _productsController.UpdateProduct(id, product) as BadRequestObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task UpdateProduct_InvalidDescription()
        {
            var id = 0;
            var product = new Product(id, "Product", "", 10, 2, 10);
            var expectedStatusCode = 400;
            var expectedError = "Failed to update product: Name or description cannot be empty";

            var res = await _productsController.UpdateProduct(id, product) as BadRequestObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task DeleteProduct_ValidId()
        {
            var expectedStatusCode = 204;
            var expectedRes = new DeleteProductResponse(true, null);

            var id = 1;
            _mockProductService.Setup(service => service.DeleteProduct(id)).ReturnsAsync(expectedRes);

            var res = await _productsController.DeleteProduct(id) as NoContentResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Test]
        public async Task DeleteProduct_InvalidId()
        {
            var id = 0;
            var expectedStatusCode = 400;
            var expectedError = $"Product with ID {id} does not exist";
            var expectedRes = new DeleteProductResponse(false, expectedError);
            
            _mockProductService.Setup(service => service.DeleteProduct(id)).ReturnsAsync(expectedRes);

            var res = await _productsController.DeleteProduct(id) as BadRequestObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(expectedStatusCode));
            Assert.That(res.Value, Is.EqualTo(expectedError));
        }
    }
}
