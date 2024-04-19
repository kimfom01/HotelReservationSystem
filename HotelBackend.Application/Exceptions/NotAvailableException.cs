namespace HotelBackend.Application.Exceptions;

public class NotAvailableException : Exception
{
    public NotAvailableException()
    {
    }

    public NotAvailableException(string message) : base(message)
    {
    }

    public NotAvailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}