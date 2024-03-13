using EFCore.BulkExtensions;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Domain.Common;
using SistemaVenta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SistemaVenta.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly SistemaVentaDbContext _context;
    

        public RepositoryBase(SistemaVentaDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public void UpdateEntity(T entity)
        {
            //_context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async void BulkUpdateEntity(List<T> list)
        {
            await _context.BulkUpdateAsync(list);
        }
        public async void BulkAddEntity(List<T> entity)
        {
            await _context.BulkInsertAsync(entity);
        }
        public async void BulkDeleteEntity(List<T> list)
        {
            await _context.BulkDeleteAsync(list);
        }
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                       string includeString = null,
                                       bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();


            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     bool disableTracking = true,
                                     params Expression<Func<T, object>>[] includes)
        {

            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();


            return await query.ToListAsync();
        }
    }
}
