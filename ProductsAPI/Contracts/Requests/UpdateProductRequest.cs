using ProductsAPI.Models;

namespace ProductsAPI.Contracts.Requests
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public Product UpdateProduct { get; set; }

        public UpdateProductRequest(int productId, Product product)
        {
            ProductId = productId;
            UpdateProduct = product;
        }
    }
}
