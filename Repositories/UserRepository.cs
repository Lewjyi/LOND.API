using LOND.API.Database;
using LOND.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LOND.API.Repositories
{
    public class UserRepository(LondDbContext dbContext) : ILondRepository<LondUser, int>
    {
        DbSet<LondUser> IBaseRepository<LondUser, int>.Entities => throw new NotImplementedException();

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<List<LondUser?>> ReturnTable()
        {
            throw new NotImplementedException();
        }
        public async Task InsertAsync(LondUser entity)
        {
            await dbContext.Lond_Users.AddAsync(entity);
            await dbContext.SaveChangesAsync();

        }
        public Task<List<LondUser>> GetAllByIdAsync(int shoporderid)
        {
            throw new NotImplementedException();
        }
        public Task<LondUser?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(LondUser entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LondUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task InsertRangeAsync(IEnumerable<LondUser> entities)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<int> itemIds)
        {
            throw new NotImplementedException();
        }
    }
}
