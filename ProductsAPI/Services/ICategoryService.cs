using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Requests.Categories;
using ProductsAPI.Contracts.Responses.Categories;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public interface ICategoryService
    {
        public Task<GetAllCategoriesResponse> GetAllCategories();
        public Task<GetCategoryByIdResponse> GetCategoryById(int id);
        public Task<Category> CreateCategory(Category category);
        public Task<UpdateCategoryResponse> UpdateCategory(int id, Category updatedCategory);
        public Task<DeleteCategoryResponse> DeleteCategory(int id);
    }
}
