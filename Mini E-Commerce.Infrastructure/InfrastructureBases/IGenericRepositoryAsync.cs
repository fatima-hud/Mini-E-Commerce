using Microsoft.EntityFrameworkCore.Storage;

namespace MiniECommerce.Infrastructure.InfrastructureBases
{
    public interface IGenericRepositoryAsync<T>
    {
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task<T?> GetByIdAsync(Guid id);
        Task DeleteRangeAsync(ICollection<T> entities);
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollBackAsync();
        Task SaveChangesAsync();

    }
}
