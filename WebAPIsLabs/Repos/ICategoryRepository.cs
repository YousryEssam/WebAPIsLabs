using WebAPIsLabs.DTO;
using WebAPIsLabs.Models;

namespace WebAPIsLabs.Repos
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Category GetByName(string name);
        public CategoryWithProductsName GetWithProductsById(int id);
    }
}
