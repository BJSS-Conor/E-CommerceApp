using Microsoft.EntityFrameworkCore;
using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Requests.Categories;
using ProductsAPI.Contracts.Responses.Categories;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private ProductCatalogueDbContext _dbContext;

        public CategoryService(ProductCatalogueDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllCategoriesResponse> GetAllCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var res = new GetAllCategoriesResponse(true, categories);

            return res;
        }

        public async Task<GetCategoryByIdResponse> GetCategoryById(int id)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            GetCategoryByIdResponse res;
            if (category == null) 
            {
                var error = $"Category with ID {id} does not exist";
                res = new GetCategoryByIdResponse(false, null, error);
            } 
            else
            {
                res = new GetCategoryByIdResponse(true, category, null);
            }
            
            return res;
        }
       
        public async Task<Category> CreateCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);            

            return category;
        }

        public async Task<UpdateCategoryResponse> UpdateCategory(int id, Category updatedCategory)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            UpdateCategoryResponse res;
            if (category != null)
            {
                category.Name = updatedCategory.Name;

                _dbContext.Entry(category).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                res = new UpdateCategoryResponse(true, updatedCategory, null);
            }
            else
            {                 
                var error = $"Category with ID {id} does not exist";
                res = new UpdateCategoryResponse(false, null, error);
            }

            return res;
        }

        public async Task<DeleteCategoryResponse> DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            
            DeleteCategoryResponse res;
            if(category != null)
            {                
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();

                res = new DeleteCategoryResponse(true, null);
            }
            else
            {
                var error = $"Unable to delete category wiht ID {id}";
                res = new DeleteCategoryResponse(false, error);
            }

            return res;
        }
    }
}
