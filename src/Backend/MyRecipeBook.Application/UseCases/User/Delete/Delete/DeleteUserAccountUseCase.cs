using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.Storage;

namespace MyRecipeBook.Application.UseCases.User.Delete.Delete;

public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
{
    private readonly IUserDeleteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;

    public DeleteUserAccountUseCase(
        IUserDeleteOnlyRepository userDeleteOnlyRepository,
        IBlobStorageService blobStorageService,
        IUnitOfWork unitOfWork)
    {
        _repository = userDeleteOnlyRepository;
        _blobStorageService = blobStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid userIdentifier)
    {
        await _blobStorageService.DelteContainer(userIdentifier);

        await _repository.DeleteAccount(userIdentifier);

        await _unitOfWork.Commit();
    }
}