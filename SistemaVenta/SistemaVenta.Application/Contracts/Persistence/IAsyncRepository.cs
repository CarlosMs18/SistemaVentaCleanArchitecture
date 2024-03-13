using SistemaVenta.Domain.Common;
using System.Linq.Expressions;

namespace SistemaVenta.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetById(int id);
        void AddEntity(T entity);
        void BulkAddEntity(List<T> entity);
        void UpdateEntity(T entity);
        void BulkUpdateEntity(List<T> list);
        void DeleteEntity(T entity);
        void BulkDeleteEntity(List<T> list);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       string includeString = null,
                                       bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     bool disableTracking = true,
                                     params Expression<Func<T, object>>[] includes);
    }
}
