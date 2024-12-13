using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Requests
{
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public Category UpdatedCategory { get; set; }

        public UpdateCategoryRequest(int categoryId, Category updatedCategory)
        {
            CategoryId = categoryId;
            UpdatedCategory = updatedCategory;
        }
    }
}
