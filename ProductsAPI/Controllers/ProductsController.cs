using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;
using ProductsAPI.Services;

namespace ProductsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductCatalogueService _productCatalogueService;

        public ProductsController(IProductCatalogueService productCatalogueService)
        {
            _productCatalogueService = productCatalogueService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productCatalogueService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var res = await _productCatalogueService.GetProductById(id);

            if(res.Success)
            {
                return Ok(res.Product);
            }
            else
            {
                return NotFound(res.Error);
            }
        }

        [HttpGet("/category/{categoryId}")]
        public async Task<ActionResult> GetProductsByCategory(int categoryId)
        {
            var res = await _productCatalogueService.GetProductsByCategory(categoryId);

            if (res.Success)
            {
                return Ok(res.Products);
            }
            else
            {
                return NotFound(res.Error);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (String.IsNullOrWhiteSpace(product.Name) || String.IsNullOrWhiteSpace(product.Description))
            {
                return BadRequest("Failed to create product: Name or Description cannot be null");
            }
            else
            {
                var res = await _productCatalogueService.CreateProduct(product);

                return CreatedAtRoute("CreateProduct", res);                
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            if (String.IsNullOrWhiteSpace(updatedProduct.Name) || String.IsNullOrWhiteSpace(updatedProduct.Description))
            {
                return BadRequest("Failed to update product: Name or description cannot be empty");
            }
            else
            {
                var res = await _productCatalogueService.UpdateProduct(id, updatedProduct);

                if (res.Success)
                {
                    return Ok(res.Product);
                }
                else
                {
                    return NotFound(res.Error);
                }                
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var res = await _productCatalogueService.DeleteProduct(id);

            if (res.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(res.Error);
            }
        }
    }
}
