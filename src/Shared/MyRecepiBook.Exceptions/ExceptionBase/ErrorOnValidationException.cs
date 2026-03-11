using System.Net;
using MyRecepiBook.Exceptions.ExceptionBase;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class ErrorOnValidationException : MyRecepiBookException
{
    private readonly IList<string> _errorMessages;

    public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty)
    {
        _errorMessages = errorMessages;
    }

    public override IList<string> GetErrorMessages() => _errorMessages;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}