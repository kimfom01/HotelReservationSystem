using Admin.Application.Dtos.Admin.Users;
using MediatR;

namespace Admin.Application.Features.Admin.Users.Commands;

public record LoginUserCommand : IRequest<LoginUserResponse>
{
    public LoginUserRequest? LoginUser { get; init; }
}