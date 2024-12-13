namespace ProductsAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(string name)
        {            
            Name = name;
        }        

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Id == category.Id &&
                   Name == category.Name;
        }
    }
}
