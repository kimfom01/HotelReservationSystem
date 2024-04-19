namespace HotelBackend.Application.Exceptions;

public class ReservationException : Exception
{
    public ReservationException()
    {
    }

    public ReservationException(string message) : base(message)
    {
    }

    public ReservationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}