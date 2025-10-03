using MyRecipeBook.Domain.Repositories;

namespace MyRecepiBook.Infrastructure.DataAccess;

public class UnitOfWork(MyRecepiBookDbContext dbContext) : IUnitOfWork
{
    public async Task Commit() => await dbContext.SaveChangesAsync();
}