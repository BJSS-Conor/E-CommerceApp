using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<GetProductResponse> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            GetProductResponse res;
            if(product == null)
            {
                res = new GetProductResponse(false, null, $"Product with ID {id} does not exist");
            }
            else
            {
                res = new GetProductResponse(true, product, null);
            }

            return res;
        }

        public async Task<GetProductsResponse> GetProductsByCategory(int categoryId)
        {
            var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

            GetProductsResponse res;
            if(products.Count == 0)
            {
                res = new GetProductsResponse(false, null, $"Category with ID {categoryId} does not exist");
            }
            else
            {
                res = new GetProductsResponse(true, products, null);
            }

            return res;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);

            return product;
        }

        public async Task<UpdateProductResponse> UpdateProduct(int id, Product updatedProduct)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == updatedProduct.Id);

            UpdateProductResponse res;
            if(product == null)
            {
                res = new UpdateProductResponse(false, null, $"Product with ID {updatedProduct.Id} does not exist");
            }
            else
            {                
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.CategoryId = updatedProduct.CategoryId;
                product.Price = updatedProduct.Price;
                product.Stock = updatedProduct.Stock;

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                res = new UpdateProductResponse(true, product, null);
            }

            return res;
        }        

        public async Task<DeleteProductResponse> DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            DeleteProductResponse res;
            if(product == null)
            {
                res = new DeleteProductResponse(false, $"Product with ID {id} does not exist");
            }
            else
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                res = new DeleteProductResponse(true, null);
            }

            return res;
        }        
    }
}
