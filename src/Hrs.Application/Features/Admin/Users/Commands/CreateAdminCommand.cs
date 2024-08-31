using Hrs.Application.Dtos.Admin.Users;
using MediatR;

namespace Hrs.Application.Features.Admin.Users.Commands;

public record CreateAdminCommand : IRequest<GetUserResponse>
{
    public CreateUserRequest? UserRequest { get; init; }
}