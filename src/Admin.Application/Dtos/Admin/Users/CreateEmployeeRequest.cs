namespace Admin.Application.Dtos.Admin.Users;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    Guid HotelId
);