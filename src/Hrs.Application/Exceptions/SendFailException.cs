namespace Hrs.Application.Exceptions;

public class SendFailException : Exception
{
    public SendFailException(string message) : base(message)
    {
    }
}