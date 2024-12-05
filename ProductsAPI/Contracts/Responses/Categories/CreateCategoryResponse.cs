using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Categories
{
    public class CreateCategoryResponse
    {
        public bool Success { get; set; }
        public Category Category { get; set; }        

        public CreateCategoryResponse(bool success, Category category)
        {
            Success = success;
            Category = category;            
        }
    }
}
