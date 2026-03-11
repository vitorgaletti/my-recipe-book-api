using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class NotFoundException(string message) : MyRecepiBookException(message)
{
    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}