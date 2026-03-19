using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Generate;

public interface IGenerateRecipeUseCase
{
    Task<ResponseGeneratedRecipeJson> Execute(RequestGenerateRecipeJson request);
}