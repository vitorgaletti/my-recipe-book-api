using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Recipe.Register;

public interface IRegisterRecipeUseCase
{
    public Task<ResponseRegiteredRecipeJson> Execute(RequestRegisterRecipeFormData request);
}