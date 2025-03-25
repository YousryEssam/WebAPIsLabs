using Microsoft.EntityFrameworkCore;
using WebAPIsLabs.DTO;
using WebAPIsLabs.Models;

namespace WebAPIsLabs.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        ITIContext dbContext;
        public CategoryRepository(ITIContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(int id)
        {
            dbContext.Categories.Remove(GetById(id));
        }

        public List<Category> GetAll()
        {
            return dbContext.Categories.AsNoTracking().ToList();
        }

        public Category GetById(int id)
        {
            return dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public Category GetByName(string name)
        {
            return dbContext.Categories.FirstOrDefault(c => c.Name == name);
        }

        public CategoryWithProductsName GetWithProductsById(int id)
        {
            Category category = dbContext.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
            if (category == null) return null;
            List<string> products = new List<string>();
            foreach(Product product in category.Products)
            {
                products.Add(product.Name);
            }
            return new CategoryWithProductsName(category, products);
        }

        public void Insert(Category obj)
        {
            dbContext.Categories.Add(obj);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void Update(Category obj)
        {
            dbContext.Categories.Update(obj);
        }
    }
}
