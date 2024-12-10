using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Products
{
    public class UpdateProductResponse
    {
        public bool Success {  get; set; }
        public Product? Product {  get; set; }
        public string? Error { get; set; }

        public UpdateProductResponse(bool success, Product? product, string? error)
        {
            Success = success;
            Product = product;
            Error = error;
        }
    }
}
