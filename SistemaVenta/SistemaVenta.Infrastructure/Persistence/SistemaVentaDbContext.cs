using Microsoft.EntityFrameworkCore;
using SistemaVenta.Domain;

namespace SistemaVenta.Infrastructure.Persistence
{
    public class SistemaVentaDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public SistemaVentaDbContext(DbContextOptions<SistemaVentaDbContext> options) : base(options) { }   
    }


}
