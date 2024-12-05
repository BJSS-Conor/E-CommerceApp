using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Categories
{
    public class GetCategoryByIdResponse
    {
        public bool Success {  get; set; }
        public Category? Category { get; set; }
        public string? Error { get; set; }
    }
}
