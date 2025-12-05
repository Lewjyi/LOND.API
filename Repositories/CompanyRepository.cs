using LOND.API.Database;
using LOND.API.Models;

namespace LOND.API.Repositories
{
    public class CompanyRepository(LondDbContext dbContext) : ILondRepository<CompanyObject, int>
    {
    }
}
