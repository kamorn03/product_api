using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class  ProductAPIDbContext : DbContext
    {
        public ProductAPIDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductGroup> productGroups { get; set; }
        public DbSet<ProductSubGroup> productSubGroups { get; set; }
    }
}
