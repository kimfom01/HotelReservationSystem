using Admin.Application.Dtos.Admin.Users;
using MediatR;

namespace Admin.Application.Features.Admin.Users.Queries;

public record GetUserQuery : IRequest<GetUserResponse>
{
    public Guid UserId { get; init; }
}