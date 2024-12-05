using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Categories
{
    public class UpdateCategoryResponse
    {
        public bool Success {  get; set; }
        public Category? UpdatedCategory { get; set; }
        public string? Error {  get; set; }
    }
}
