using Hrs.Application.Dtos.Admin.Users;
using MediatR;

namespace Hrs.Application.Features.Admin.Users.Commands;

public record CreateUserCommand : IRequest<GetUserResponse>
{
    public CreateUserRequest? UserRequest { get; init; }
}