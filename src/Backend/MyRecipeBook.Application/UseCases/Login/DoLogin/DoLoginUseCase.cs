using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncripter passwordEncripter) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;

    private readonly PasswordEncripter _passwordEncripter = passwordEncripter;

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encryptedPassword = _passwordEncripter.Encrypt(request.Password);

        var user = await _repository.GetByEmailAndPassword(request.Email, encryptedPassword);

        if (user is null)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }
}