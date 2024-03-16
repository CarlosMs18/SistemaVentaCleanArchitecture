using SistemaVenta.Domain;

namespace SistemaVenta.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByUserId();       
    }
}
