using Microsoft.EntityFrameworkCore.Storage;
using SistemaVenta.Application.Contracts.Persistence;
using SistemaVenta.Domain.Common;
using SistemaVenta.Infrastructure.Persistence;
using System.Collections;

namespace SistemaVenta.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly SistemaVentaDbContext _context;
        private IDbContextTransaction transaction;

        public UnitOfWork(SistemaVentaDbContext context)
        {
            _context = context;
        }

        public SistemaVentaDbContext SistemaVentaDbContext => _context;

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> ExecStoreProcedure(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }
        public void Dispose()

        {
            _context.Dispose();
        }

        public async Task Rollback()
        {
            await transaction.RollbackAsync();
        }

        public async Task Commit()
        {
            await transaction.CommitAsync();
        }

        public async Task BeginTransaction()
        {
            transaction = await _context.Database.BeginTransactionAsync();
        }
        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
