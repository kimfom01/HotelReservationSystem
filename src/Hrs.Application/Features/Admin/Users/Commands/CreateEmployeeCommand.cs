using Hrs.Application.Dtos.Admin.Users;
using MediatR;

namespace Hrs.Application.Features.Admin.Users.Commands;

public class CreateEmployeeCommand : IRequest<GetUserResponse>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string ConfirmPassword { get; init; } = string.Empty;
    public Guid HotelId { get; init; }
}