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

            var res = await _categoryService.CreateCategory(category);

            Assert.That(res.Name, Is.EqualTo(category.Name));                       
        }

        [Test]
        public async Task UpdateCategory_ValidCategory()
        {
            var updatedCategory = new Category(3, "Updated Category");            

            var res = await _categoryService.UpdateCategory(3, updatedCategory);
            var category = await _categoryService.GetCategoryById(3);

            Assert.That(res.Success, Is.True);
            Assert.That(res.UpdatedCategory, Is.EqualTo(updatedCategory));
            Assert.That(res.Error, Is.Null);
            Assert.That(category.Category, Is.EqualTo(updatedCategory));
        }

        [Test]
        public async Task UpdateCategory_InvalidCategory()
        {
            var updatedCategory = new Category(10, "Updated Category");            
            var expectedError = $"Category with ID {updatedCategory.Id} does not exist";

            var res = await _categoryService.UpdateCategory(10, updatedCategory);

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
            Assert.That(res.Error, Is.Null);
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
            var expectedError = $"Unable to delete category wiht ID {categoryId}";

            var res = await _categoryService.DeleteCategory(categoryId);

            Assert.That(res.Success, Is.False);
            Assert.That(res.Error, Is.EqualTo(expectedError));
        }

        [TearDown]
        public void Teardown()
        {
            _productCatalogueDbContext.Dispose();
        }
    }
}
