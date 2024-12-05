using ProductsAPI.Contracts.Requests;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task<Category> UpdateCategory(UpdateCategoryRequest req);
        public Task DeleteCategory(int id);
    }
}
