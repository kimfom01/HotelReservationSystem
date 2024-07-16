using Hrs.Application.Dtos.Admin.Users;
using MediatR;

namespace Hrs.Application.Features.Admin.Users.Queries;

public record GetUserQuery : IRequest<GetUserResponse>
{
    public Guid UserId { get; init; }
}