using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;

namespace MyRecipeBook.Application.UseCases.Token.RefreshToken;

public interface IUseRefreshTokenUseCase
{
    Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
}