using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Domain;
using SistemaVenta.Infrastructure.Persistence;

namespace SistemaVenta.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(SistemaVentaDbContext context) : base(context)
        {
        }
    }
}
