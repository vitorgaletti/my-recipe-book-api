using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class UnauthorizedException : MyRecepiBookException
{
    private readonly Func<string>? _messageProvider;

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(Func<string> messageProvider) : base(string.Empty)
    {
        _messageProvider = messageProvider;
    }

    public override IList<string> GetErrorMessages()
    {
        if (_messageProvider is not null)
            return [_messageProvider()];
        
        return [Message];
    }

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}