using Hrs.Application.Dtos.Admin.Users;
using MediatR;

namespace Hrs.Application.Features.Admin.Users.Commands;

public record LoginUserCommand : IRequest<LoginUserResponse>
{
    public LoginUserRequest? LoginUser { get; init; }
}