using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using FluentAssertions;
using MyRecepiBook.Exceptions;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Application.UseCases.User.Register;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Name.Should().NotBeNullOrEmpty();
        result.Name.Should().Be(request.Name);
    }
    
    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestRegisterUserJsonBuilder.Build();

        var useCase = CreateUseCase(request.Email); 
        
        Func<Task> act = async () =>  await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(e => e.GetErrorMessages().Count.Equals(1) && e.GetErrorMessages().Contains(ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
    }
    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase(); 
        
        Func<Task> act = async () =>  await useCase.Execute(request);

        (await act.Should().ThrowAsync<ErrorOnValidationException>()).Where(e => e.GetErrorMessages().Count.Equals(1) && e.GetErrorMessages().Contains(ResourceMessagesException.NAME_EMPTY));
    }

    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var writeRepository = UserWriteOnlyRespositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (!string.IsNullOrEmpty(email))
            readRepositoryBuilder.ExistActiveUserWithEmail(email);
        
        return new RegisterUserUseCase(writeRepository, readRepositoryBuilder.Build(), unitOfWork, mapper, passwordEncripter, accessTokenGenerator);
    }
}