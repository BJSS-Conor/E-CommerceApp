using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Requests.Categories
{
    public class CreateCategoryRequest
    {
        public Category Category { get; set; }

        public CreateCategoryRequest(Category category)
        {
            Category = category;
        }
    }
}
