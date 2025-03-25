using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIsLabs.DTO
{
    public class AddProduc
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public string? Description { set; get; }
        public int CategoryId { set; get; }
    }
}
