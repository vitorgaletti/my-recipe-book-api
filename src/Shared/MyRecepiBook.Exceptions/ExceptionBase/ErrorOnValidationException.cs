using MyRecepiBook.Exceptions.ExceptionBase;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public class ErrorOnValidationException(IList<string> errorMessages) : MyRecepiBookException(string.Empty)
{
    public IList<string> ErrorMessages { get; set; } = errorMessages;
}