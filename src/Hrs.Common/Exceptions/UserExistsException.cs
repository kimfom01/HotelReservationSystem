namespace Hrs.Common.Exceptions;

public class UserExistsException : Exception
{
    public UserExistsException(string message) : base(message)
    {
    }

    public UserExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}