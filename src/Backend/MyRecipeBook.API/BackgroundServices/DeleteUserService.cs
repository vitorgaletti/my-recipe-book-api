using Azure.Messaging.ServiceBus;
using MyRecepiBook.Infrastructure.Services.ServiceBus;
using MyRecipeBook.Application.UseCases.User.Delete.Delete;

namespace MyRecipeBook.API.BackgroundServices;

public class DeleteUserService : BackgroundService
{
    private readonly IServiceProvider _services;

    private readonly ServiceBusProcessor _processor;

    public DeleteUserService(IServiceProvider services, DeleteUserProcessor processor)
    {
        _processor = processor.GetProcessor();
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += ProcessMessageAsync;

        _processor.ProcessErrorAsync += ExceptionReceivedHandler;

        await _processor.StartProcessingAsync(stoppingToken);
    }

    private static Task ExceptionReceivedHandler(ProcessErrorEventArgs _) => Task.CompletedTask;

    private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
    {
        var message = eventArgs.Message.Body.ToString();

        var userIdentifier = Guid.Parse(message);

        var scope = _services.CreateScope();

        var deleteUserCase = scope.ServiceProvider.GetRequiredService<IDeleteUserAccountUseCase>();

        await deleteUserCase.Execute(userIdentifier);
    }

    ~DeleteUserService() => Dispose();

    public override void Dispose()
    {
        base.Dispose();

        GC.SuppressFinalize(this);
    }
}