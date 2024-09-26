namespace Hrs.Common.Exceptions;

public class SendFailException : Exception
{
    public SendFailException(string message) : base(message)
    {
    }
}