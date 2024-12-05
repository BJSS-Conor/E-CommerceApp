using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Requests
{
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public Category UpdatedCategory { get; set; }
    }
}
