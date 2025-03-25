using WebAPIsLabs.Models;

namespace WebAPIsLabs.DTO
{
    public class ProductWithCategoryName
    {
        public ProductWithCategoryName() { }
        public ProductWithCategoryName(Product product) {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Category = product.Category.Name;
        }
        public int Id { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public string? Description { set; get; } 
        public string Category  { set; get; }
    }
}
