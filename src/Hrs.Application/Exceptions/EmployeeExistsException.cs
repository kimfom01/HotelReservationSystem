namespace Hrs.Application.Exceptions;

public class EmployeeExistsException : Exception
{
    public EmployeeExistsException(string message) : base(message)
    {
    }

    public EmployeeExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}