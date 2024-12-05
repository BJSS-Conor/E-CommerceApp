using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Requests.Categories;
using ProductsAPI.Contracts.Responses.Categories;

namespace ProductsAPI.Services
{
    public interface ICategoryService
    {
        public Task<GetAllCategoriesResponse> GetAllCategories();
        public Task<GetCategoryByIdResponse> GetCategoryById(int id);
        public Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest req);
        public Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest req);
        public Task<DeleteCategoryResponse> DeleteCategory(int id);
    }
}
