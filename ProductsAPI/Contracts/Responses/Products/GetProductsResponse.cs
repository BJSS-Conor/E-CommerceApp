using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Responses.Products
{
    public class GetProductsResponse
    {
        public bool Success { get; set; }
        public List<Product> Products { get; set; }
        public string Error { get; set; }

        public GetProductsResponse(bool success, List<Product> products, string error)
        {
            Success = success;
            Products = products;
            Error = error;
        }
    }
}
