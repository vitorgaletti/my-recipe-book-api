using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class InvalidLoginException : MyRecepiBookException
{
    public InvalidLoginException() : base(string.Empty)
    {
    }

    public override IList<string> GetErrorMessages() => [ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}