namespace MyRecepiBook.Exceptions.ExceptionBase;

public class InvalidLoginException : MyRecepiBookException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}