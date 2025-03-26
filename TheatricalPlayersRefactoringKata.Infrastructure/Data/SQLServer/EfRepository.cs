using Valhalla.Lib.SharedKernel;
using Valhalla.Lib.Specification.EntityFrameworkCore;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Data.SQLServer;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(SqlDbContext dbContext) : base(dbContext)
    {
    }
}
