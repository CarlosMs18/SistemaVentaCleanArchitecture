using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Domain;
using SistemaVenta.Identity;
using SistemaVenta.Infrastructure.Persistence;

namespace SistemaVenta.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(SistemaVentaDbContext context, SistemaVentaIdentityDbContext identityDbContext) : base(context, identityDbContext)
        {
        }
    }
}
