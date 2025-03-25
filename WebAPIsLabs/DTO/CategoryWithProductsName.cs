using Azure.Core.GeoJson;
using WebAPIsLabs.Models;

namespace WebAPIsLabs.DTO
{
    public class CategoryWithProductsName
    {
        public CategoryWithProductsName() { 
        }
        public CategoryWithProductsName(Category category , List<string > prs)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
            Products = prs;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<string>? Products { get; set; }
    }
}
