using WebAPIsLabs.Models;

namespace WebAPIsLabs.Repos
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetProductCategoryById(int id);
    }
}
