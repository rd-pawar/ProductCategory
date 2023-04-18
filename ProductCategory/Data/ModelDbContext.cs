using Microsoft.EntityFrameworkCore;
using ProductCategory.Models.Domain;

namespace ProductCategory.Data
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
