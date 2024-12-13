using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Categories
{
    public class GetAllCategoriesResponse
    {
        public bool Success {  get; set; }
        public List<Category> Categories { get; set; }  
        
        public GetAllCategoriesResponse(bool success, List<Category> categories)
        {
            Success = success;
            Categories = categories;
        }
    }
}
