using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class RefreshTokenNotFoundException : MyRecepiBookException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}