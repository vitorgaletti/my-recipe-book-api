using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(
    IUserReadOnlyRepository repository,
    IPasswordEncripter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var user = await repository.GetByEmail(request.Email);

        if (user is null || passwordEncripter.IsValid(request.Password, user.Password).IsFalse())
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }
}