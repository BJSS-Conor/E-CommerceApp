using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.UnitTest.Services
{
    internal class UT_ProductService
    {
        private IProductCatalogueService _productCatalogueService;
        private ProductCatalogueDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogueDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ProductCatalogueDbContext(options);

            _dbContext.Products.AddRange(new List<Product>
            {
                new Product(1, "Product 1", "Description 1", 10, 1, 50),
                new Product(2, "Product 2", "Description 2", 20, 2, 100),
                new Product(3, "Product 3", "Description 3", 30, 2, 5),
            });
            _dbContext.SaveChanges();

            _productCatalogueService = new ProductCatalogueService(_dbContext);
        }

        [Test]
        public async Task GetAllProducts_Test()
        {
            var expectedProducts = new List<Product>()
            {
                new Product(1, "Product 1", "Description 1", 10, 1, 50),
                new Product(2, "Product 2", "Description 2", 20, 2, 100),
                new Product(3, "Product 3", "Description 3", 30, 2, 5),
            };

            var products = await _productCatalogueService.GetAllProducts();

            Assert.That(products, Is.EqualTo(expectedProducts));
        }

        [Test]
        public async Task GetProductById_ValidId()
        {
            var index = 2;
            var expectedProduct = new Product(index, "Product 2", "Description 2", 20, 2, 100);

            var res = await _productCatalogueService.GetProductById(index);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Product, Is.EqualTo(expectedProduct));
            Assert.That(res.Error, Is.Null);
        }

        [TestCase(0)]
        [TestCase(4)]
        public async Task GetProductById_InvalidId(int id)
        {            
            var expectedError = $"Product with ID {id} does not exist";

            var res = await _productCatalogueService.GetProductById(id);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Product, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task GetProductsByCategory_ValidId()
        {
            var categoryId = 2;
            var expectedProducts = new List<Product>() 
            {
                new Product(2, "Product 2", "Description 2", 20, 2, 100),
                new Product(3, "Product 3", "Description 3", 30, 2, 5)
            };

            var res = await _productCatalogueService.GetProductsByCategory(categoryId);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Products, Is.EqualTo(expectedProducts));
            Assert.That(res.Error, Is.Null);
        }

        [TestCase(0)]
        [TestCase(4)]
        public async Task GetProductsByCategory_InvalidId(int categoryId)
        {
            var expectedError = $"Category with ID {categoryId} does not exist";

            var res = await _productCatalogueService.GetProductsByCategory(categoryId);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Products, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task CreateProduct_Test()
        {
            var product = new Product(4, "Product 4", "Description 4", 40, 2, 2);

            var res = await _productCatalogueService.CreateProduct(product);

            Assert.That(res, Is.EqualTo(product));
        }

        [Test]
        public async Task UpdateProduct_ValidId()
        {
            var productId = 2;
            var updatedProduct = new Product(productId, "Updated Name", "Updated Description", 5, 2, 5);

            var res = await _productCatalogueService.UpdateProduct(productId, updatedProduct);
            var getProductRes = await _productCatalogueService.GetProductById(productId);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Product, Is.EqualTo(updatedProduct));
            Assert.That(res.Error, Is.Null);
            Assert.That(getProductRes.Product, Is.EqualTo(updatedProduct));
        }        

        [TestCase(0)]
        [TestCase(4)]
        public async Task UpdateProduct_InvalidId(int productId)
        {                        
            var updatedProduct = new Product(productId, "Updated Name", "Updated Description", 5, 2, 5);
            var expectedError = $"Product with ID {productId} does not exist";

            var res = await _productCatalogueService.UpdateProduct(productId, updatedProduct);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Product, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }               

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(2)]
        public async Task DeleteProduct_ValidId(int productId)
        {
            await _productCatalogueService.DeleteProduct(productId);
            
            var getProductRes = await _productCatalogueService.GetProductById(productId);

            Assert.That(getProductRes.Success, Is.False);
        }

        [TestCase(0)]
        [TestCase(4)]
        public async Task DeleteProduct_InvalidId(int productId)
        {            
            var expectedError = $"Product with ID {productId} does not exist";

            var res = await _productCatalogueService.DeleteProduct(productId);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }
    }
}
