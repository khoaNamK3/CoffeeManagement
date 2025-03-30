using CoffeeManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CoffeeManagement.Repositories.Implement
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        public TContext _context { get;}

        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        #region Repository Management
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class 
        { 
            _repositories = new Dictionary<Type, object>();
            if (_repositories.TryGetValue(typeof(TEntity), out object repository))
            {
                return (IGenericRepository<TEntity>)repository;
            }
            repository = new GenericRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return (IGenericRepository<TEntity>) repository;
        }


        #endregion

        #region Transaction Management
        public async Task<T> ExcuteInTransactionAsync<T>(Func<Task<T>> operation)
        {
            var executionStratery = _context.Database.CreateExecutionStrategy();
            return await executionStratery.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var result = await operation();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
            
        public async Task ExecuteInTransactionAsync(Func<Task> opteration)
        {
            var executionStratery = _context.Database.CreateExecutionStrategy();
             await executionStratery.ExecuteAsync(async () =>
             {
                 await using var transaction = await _context.Database.BeginTransactionAsync();
                 try
                 {
                     await opteration();
                     await _context.SaveChangesAsync();
                     await transaction.CommitAsync();
                     
                 }
                 catch
                 {
                     await transaction.RollbackAsync();
                     throw;
                 }

             });
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
