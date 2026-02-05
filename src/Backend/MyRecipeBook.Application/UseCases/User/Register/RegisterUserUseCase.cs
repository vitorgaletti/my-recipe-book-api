using AutoMapper;
using FluentValidation.Results;
using MyRecepiBook.Communication.Requests;
using MyRecepiBook.Communication.Responses;
using MyRecepiBook.Exceptions;
using MyRecepiBook.Exceptions.ExceptionBase;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase(IUserWriteOnlyRepository writeOnlyRepository,
                                 IUserReadOnlyRepository readOnlyRepository,
                                 IUnitOfWork unitOfWork,
                                 IMapper mapper,
                                 PasswordEncripter passwordEncripter,
                                 IAccessTokenGenerator accessTokenGenerator) : IRegisterUserUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        
        await Validate(request);
        
        var user = mapper.Map<Domain.Entities.User>(request);
        user.Password = passwordEncripter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await writeOnlyRepository.Add(user);
        await unitOfWork.Commit();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = accessTokenGenerator.Generate(user.UserIdentifier)
            }
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        
        var result = validator.Validate(request);

        var emailExist = await readOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
        
        throw new ErrorOnValidationException(errorMessages);
    }
}