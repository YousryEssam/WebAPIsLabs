using Microsoft.EntityFrameworkCore;
using WebAPIsLabs.Models;

namespace WebAPIsLabs.Repos
{
    public class ProductRepository : IProductRepository
    {

        private ITIContext _dbContext;
        public ProductRepository(ITIContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            _dbContext.Products.Remove(GetById(id));
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products.AsNoTracking().ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public Product GetProductCategoryById(int id)
        {
            return _dbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public void Insert(Product obj)
        {
            _dbContext.Products.Add(obj);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Product obj)
        {
            _dbContext.Products.Update(obj);
        }
    }
}
