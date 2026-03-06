using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter;

public interface IFilterRecipeUseCase
{
    Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request);
}