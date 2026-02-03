using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}