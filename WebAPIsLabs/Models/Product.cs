using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace WebAPIsLabs.Models
{
    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public string? Description { set; get; }
        [ForeignKey("Category")]
        public int CategoryId { set; get; }
        public Category? Category { set; get; }

    }
}
