using ProductsAPI.Contracts.Requests;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public interface IProductCatalogueService
    {
        public Task<List<Product>> GetAllProducts();
        public Task<Product> GetProductById(int id);
        public Task<Product> UpdateProduct(UpdateProductRequest req); 
        public Task DeleteProduct(int id);
        public Task ReduceStock(int reduction);
    }
}
