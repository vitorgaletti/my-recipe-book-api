using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class RefreshTokenExpiredException : MyRecepiBookException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
    {
    }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}