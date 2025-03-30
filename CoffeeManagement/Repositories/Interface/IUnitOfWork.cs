using Microsoft.EntityFrameworkCore;

namespace CoffeeManagement.Repositories.Interface
{
    public interface IUnitOfWork: IGenericRepositoryFactory, IDisposable
    {
        public Task<T> ExcuteInTransactionAsync<T>(Func<Task<T>> operation);
        public Task ExecuteInTransactionAsync(Func<Task> opteration);
    }
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext _context { get; }
    }
}
