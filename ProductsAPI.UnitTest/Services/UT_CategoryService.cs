using Microsoft.EntityFrameworkCore;
using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Requests.Categories;
using ProductsAPI.Models;
using ProductsAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAPI.UnitTest.Services
{
    public class UT_CategoryService
    {
        private ICategoryService _categoryService;
        private ProductCatalogueDbContext _productCatalogueDbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ProductCatalogueDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _productCatalogueDbContext = new ProductCatalogueDbContext(options);

            _productCatalogueDbContext.Categories.AddRange(new List<Category>
            {
                new Category(1, "Category 1"),
                new Category(2, "Category 2"),
                new Category(3, "Category 3"),
            });
            _productCatalogueDbContext.SaveChanges();

            _categoryService = new CategoryService(_productCatalogueDbContext);
        }

        [Test]
        public async Task GetAllCategories_Test()
        {
            var expectedCategories = new List<Category>()
            {
                new Category(1, "Category 1"),
                new Category(2, "Category 2"),
                new Category(3, "Category 3"),
            };

            var res = await _categoryService.GetAllCategories();

            Assert.That(res.Success, Is.True);
            Assert.That(res.Categories, Is.EqualTo(expectedCategories));            
        }

        [Test]
        public async Task GetCategoryById_ValidId()
        {
            var categoryId = 2;
            var expectedCategory = new Category(2, "Category 2");

            var res = await _categoryService.GetCategoryById(categoryId);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Category, Is.EqualTo(expectedCategory));
            Assert.That(res.Error, Is.Null);
        }

        [Test]
        public async Task GetCategoryById_InvalidId() 
        {
            var categoryId = 0;
            var expectedError = $"Category with ID {categoryId} does not exist";

            var res = await _categoryService.GetCategoryById(categoryId);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Category, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task CreateCategory_Test()
        {
            var category = new Category("TestCategory");
            var req = new CreateCategoryRequest(category);

            var res = await _categoryService.CreateCategory(req);

            Assert.That(res.Success, Is.True);
            Assert.That(res.Category, Is.EqualTo(category));            
        }

        [Test]
        public async Task UpdateCategory_ValidCategory()
        {
            var category = new Category(3, "Updated Category");
            var req = new UpdateCategoryRequest(3, category);

            var res = await _categoryService.UpdateCategory(req);

            Assert.That(res.Success, Is.True);
            Assert.That(res.UpdatedCategory, Is.EqualTo(category));
            Assert.That(res.Error, Is.False);
        }

        [Test]
        public async Task UpdateCategory_InvalidCategory()
        {
            var category = new Category(10, "Updated Category");
            var req = new UpdateCategoryRequest(10, category);
            var expectedError = $"Category with ID {category.Id} does not exist";

            var res = await _categoryService.UpdateCategory(req);

            Assert.That(res.Success, Is.False);
            Assert.That(res.UpdatedCategory, Is.Null);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [Test]
        public async Task DeleteService_ValidId()
        {
            var categoryId = 2;
            var expectedCategories = new List<Category>()
            {
                new Category(1, "Category 1"),                
                new Category(3, "Category 3"),
            };

            var res = await _categoryService.DeleteCategory(categoryId);

            Assert.That(res.Success, Is.True);
        }

        [Test]
        public async Task DeleteService_InvalidId()
        {
            var categoryId = 0;
            var expectedCategories = new List<Category>()
            {
                new Category(1, "Category 1"),
                new Category(3, "Category 3"),
            };

            var res = await _categoryService.DeleteCategory(categoryId);

            Assert.That(res.Success, Is.False);
        }

        [TearDown]
        public void Teardown()
        {
            _productCatalogueDbContext.Dispose();
        }
    }
}
