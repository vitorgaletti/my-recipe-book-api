using CommonTestUtilities.Entities;
using CommonTestUtilities.IdEncryption;
using CommonTestUtilities.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecepiBook.Infrastructure.DataAccess;
using MyRecipeBook.Domain.Enums;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private MyRecipeBook.Domain.Entities.User _user = default!;
    private string _password = string.Empty;
    private MyRecipeBook.Domain.Entities.Recipe _recipe = default!;
    
    public string GetEmail() => _user.Email;
    public string GetPassword() => _password;
    public string GetName() => _user.Name;
    public Guid GetUserIdentifier() => _user.UserIdentifier;
    
    public string GetRecipeId() => IdEncripterBuilder.Build().Encode(_recipe.Id);
    public string GetRecipeTitle() => _recipe.Title;
    public Difficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
    public CookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
    public IList<DishType> GetDishTypes() => _recipe.DishTypes.Select(c => c.Type).ToList();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MyRecepiBookDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MyRecepiBookDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                services.AddScoped(options => JwtTokenGeneratorBuilder.Build());

                using var scope = services.BuildServiceProvider().CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<MyRecepiBookDbContext>();
                dbContext.Database.EnsureDeleted();

                StartDatabase(dbContext);
            });
    }

    private void StartDatabase(MyRecepiBookDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();
        
        _recipe = RecipeBuilder.Build(_user);

        dbContext.Users.Add(_user);
        
        dbContext.Recipes.Add(_recipe);

        dbContext.SaveChanges();
    }
}