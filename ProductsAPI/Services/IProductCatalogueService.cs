using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Responses.Products;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public interface IProductCatalogueService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<GetProductResponse> GetProductById(int id);
        public Task<GetProductsResponse> GetProductsByCategory(int categoryId);
        public Task<Product> CreateProduct(Product product);
        public Task<UpdateProductResponse> UpdateProduct(int id, Product product); 
        public Task<DeleteProductResponse> DeleteProduct(int id);
        public Task ReduceStock(int reduction);
    }
}
