namespace Demo.Restuarants.Shared.Exceptions;

public class BaseCustomException : Exception
{
    public string UserMessage { get; }

    public BaseCustomException(string msg)
        : base(msg)
    {
        UserMessage = msg;
    }

    public BaseCustomException(string msg, Exception innerException)
        : base(msg, innerException)
    {
        UserMessage = msg;
    }
}
