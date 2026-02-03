using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FluentAssertions;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Exceptions;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Application.UseCases.Login.DoLogin;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        
        var useCase = CreateUseCase(user);

        var result = await useCase.Execute(new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        });

        result.Should().NotBeNull();
        result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
    }

    [Fact]
    public async Task Error_Invalid_Use()
    {
        var request = RequestLoginJsonBuilder.Build();

        var (user, password) = UserBuilder.Build();
        
        var useCase = CreateUseCase(user);

        Func<Task> act = async () => await useCase.Execute(request);

        await act.Should().ThrowAsync<InvalidLoginException>()
            .Where(e => e.Message.Equals(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID));
    }

    private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        
        if (user is not null)
            userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);

        return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(), passwordEncripter);
    }
}