using System.Net;

namespace MyRecepiBook.Exceptions.ExceptionBase;

public abstract class MyRecepiBookException : SystemException
{
    protected MyRecepiBookException(string message) : base(message) { }
    
    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}