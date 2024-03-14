using SistemaVenta.Domain.Common;

namespace SistemaVenta.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;
        Task<int> Complete();
        Task<int> ExecStoreProcedure(string sql, params object[] parameters);
        Task Rollback();
        Task Commit();
        Task BeginTransaction();
    }
}
