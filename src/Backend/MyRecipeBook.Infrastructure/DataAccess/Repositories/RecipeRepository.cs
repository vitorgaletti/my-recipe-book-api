using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecepiBook.Infrastructure.DataAccess.Repositories;

public sealed class RecipeRepository : IRecipeWriteOnlyRepository
{
    private readonly MyRecepiBookDbContext _dbContext;

    public RecipeRepository(MyRecepiBookDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);
}