using Microsoft.EntityFrameworkCore;
using SistemaVenta.Application.Models.Identity;
using SistemaVenta.Domain;

namespace SistemaVenta.Infrastructure.Persistence
{
    public class SistemaVentaDbContext : DbContext
    {
        private readonly UserSession userSession;
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public SistemaVentaDbContext(DbContextOptions<SistemaVentaDbContext> options, UserSession userSession) : base(options) { 
            this.userSession = userSession; 
        }   
    }


}
