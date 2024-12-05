using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;

namespace ProductsAPI
{
    public class ProductCatalogueDbContext : DbContext
    {
        public ProductCatalogueDbContext(DbContextOptions<ProductCatalogueDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
