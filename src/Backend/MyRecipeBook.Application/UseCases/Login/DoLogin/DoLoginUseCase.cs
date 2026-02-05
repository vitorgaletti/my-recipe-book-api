using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(
    IUserReadOnlyRepository repository,
    PasswordEncripter passwordEncripter,
    IAccessTokenGenerator accessTokenGenerator) : IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = passwordEncripter.Encrypt(request.Password);

        var user = await repository.GetByEmailAndPassword(request.Email, encryptedPassword);

        if (user is null)
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