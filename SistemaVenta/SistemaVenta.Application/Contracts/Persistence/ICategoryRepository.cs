using SistemaVenta.Domain;

namespace SistemaVenta.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
    }
}
