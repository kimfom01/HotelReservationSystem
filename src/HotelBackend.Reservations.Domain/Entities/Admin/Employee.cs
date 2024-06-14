using HotelBackend.Reservations.Domain.Entities.Common;

namespace HotelBackend.Reservations.Domain.Entities.Admin;

public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}