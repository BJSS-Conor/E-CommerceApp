using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Requests
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public Product UpdatedProduct { get; set; }

        public UpdateProductRequest(int productId, Product updatedProduct)
        {
            ProductId = productId;
            UpdatedProduct = updatedProduct;
        }
    }
}
