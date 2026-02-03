using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecepiBook.Infrastructure.DataAccess.Repositories;

public class UserRepository(MyRecepiBookDbContext dbContext) : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    public async Task Add(User user) => await dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user =>
            user.Email.Equals(email) && user.Password.Equals(password) && user.IsActive);
    }
}