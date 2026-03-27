using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecepiBook.Infrastructure.DataAccess.Repositories;

public class UserRepository(MyRecepiBookDbContext dbContext) : IUserWriteOnlyRepository, IUserReadOnlyRepository,
    IUserUpdateOnlyRepository, IUserDeleteOnlyRepository
{
    public async Task Add(User user) => await dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) =>
        await dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.IsActive);

    public async Task<User?> GetByEmailAndPassword(string email, string password)
    {
        return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user =>
            user.Email.Equals(email) && user.Password.Equals(password) && user.IsActive);
    }

    public async Task<bool> ExistActiveUserWithIdentifier(Guid userIdentifier) =>
        await dbContext.Users.AnyAsync(user => user.UserIdentifier.Equals(userIdentifier) && user.IsActive);

    public async Task<User> GetById(long id)
    {
        return await dbContext.Users.FirstAsync(user => user.Id == id);
    }

    public void Update(User user) => dbContext.Users.Update(user);

    public async Task DeleteAccount(Guid userIdentifier)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);
        
        if (user is null)
            return;

        var recipes = dbContext.Recipes.Where(recipe => recipe.UserId == user.Id);

        dbContext.Recipes.RemoveRange(recipes);

        dbContext.Users.Remove(user);
    }
}