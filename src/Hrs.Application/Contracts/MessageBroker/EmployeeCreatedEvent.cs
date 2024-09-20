namespace Hrs.Application.Contracts.MessageBroker;

public record EmployeeCreatedEvent(
    string FullName,
    string Email,
    string Password,
    Guid HotelId
);