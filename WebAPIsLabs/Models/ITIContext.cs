using Microsoft.EntityFrameworkCore;

namespace WebAPIsLabs.Models
{
    public class ITIContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options) { 

        }
    }
}
