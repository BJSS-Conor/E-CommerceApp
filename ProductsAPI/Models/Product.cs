namespace ProductsAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }

        public Product(int id, string name, string description, int price, int categoryId, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Stock = stock;
        }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   Id == product.Id &&
                   Name == product.Name &&
                   Description == product.Description &&
                   Price == product.Price &&
                   CategoryId == product.CategoryId &&
                   Stock == product.Stock;
        }
    }
}
