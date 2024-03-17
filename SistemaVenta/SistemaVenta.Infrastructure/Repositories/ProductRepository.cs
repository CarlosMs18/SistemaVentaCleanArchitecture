using Microsoft.EntityFrameworkCore;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Domain;
using SistemaVenta.Identity;
using SistemaVenta.Infrastructure.Persistence;

namespace SistemaVenta.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(SistemaVentaDbContext context, SistemaVentaIdentityDbContext identityDbContext) : base(context, identityDbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByUserId()
        {
            var products = await _context.Products
                                     .Include(x => x.Category)
                                     .AsNoTracking()
                                     .ToListAsync();
            return products;
        }
    }
}
