namespace SistemaVenta.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        Task<int> ExecStoreProcedure(string sql, params object[] parameters);
        Task Rollback();
        Task Commit();
        Task BeginTransaction();
    }
}
