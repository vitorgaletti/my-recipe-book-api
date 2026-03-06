using Bogus;
using MyRecepiBook.Communication.Requests;
using MyRecipeBook.Domain.Enums;

namespace CommonTestUtilities.Requests;

public class RequestFilterRecipeJsonBuilder
{
    public static RequestFilterRecipeJson Build()
    {
        return new Faker<RequestFilterRecipeJson>()
            .RuleFor(user => user.CookingTimes, f => f.Make(1, () => f.PickRandom<CookingTime>()))
            .RuleFor(user => user.Difficulties, f => f.Make(1, () => f.PickRandom<Difficulty>()))
            .RuleFor(user => user.DishTypes, f => f.Make(1, () => f.PickRandom<DishType>()))
            .RuleFor(user => user.RecipeTitle_Ingredient, f => f.Lorem.Word());
    }
}