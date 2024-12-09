using ProductsAPI.Contracts.Requests;
using ProductsAPI.Contracts.Responses.Products;
using ProductsAPI.Models;

namespace ProductsAPI.Services
{
    public class ProductCatalogueService : IProductCatalogueService
    {
        private ProductCatalogueDbContext _context;

        public ProductCatalogueService(ProductCatalogueDbContext context)
        {
            _context = context;
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task ReduceStock(int reduction)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(UpdateProductRequest req)
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductCatalogueService.CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        Task<DeleteProductResponse> IProductCatalogueService.DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        Task<GetProductResponse> IProductCatalogueService.GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        Task<GetProductsResponse> IProductCatalogueService.GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        Task<UpdateProductResponse> IProductCatalogueService.UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
