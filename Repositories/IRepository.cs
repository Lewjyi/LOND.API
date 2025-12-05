using LOND.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LOND.API.Repositories
{

    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TKey itemId);
        Task DeleteRangeAsync(IEnumerable<TKey> itemIds);
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).ExecuteDeleteAsync();
        }
    }

    public interface IRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(TKey id);
    }

    public interface ILondRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        Task<List<TEntity>> ReturnTable();
    }
}


