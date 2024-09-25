using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Users;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Users.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
{
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserQueryHandler> _logger;

    public GetUserQueryHandler(
        IAdminUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GetUserQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetUserResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting user with id={UserId}", query.UserId);

        var user = await _unitOfWork.Users.GetEntity(usr => usr.Id == query.UserId, cancellationToken);

        if (user is null)
        {
            _logger.LogError("User with id={UserId} does not exist", query.UserId);
            throw new NotFoundException($"User with id={query.UserId} does not exist");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
}