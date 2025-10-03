using MyRecepiBook.Exceptions.ExceptionBase;

namespace MyRecepiBook.Exceptions;

public class ErrorOnValidationException(IList<string> errorMessages) : MyRecepiBookException
{
    public IList<string> ErrorMessages { get; set; } = errorMessages;
}