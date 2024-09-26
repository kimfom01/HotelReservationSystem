using Admin.Application.Dtos.Admin.Users;
using MediatR;

namespace Admin.Application.Features.Admin.Users.Commands;

public record CreateAdminCommand : IRequest<GetUserResponse>
{
    public CreateUserRequest? UserRequest { get; init; }
}