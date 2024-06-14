using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Reservations;
using Hrs.Application.Features.Reservations.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Reservations.Handlers.Queries;

public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<GetReservationDetailsResponse>>
{
    private readonly ILogger<GetAllReservationsQueryHandler> _logger;
    private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
    private readonly IMapper _mapper;

    public GetAllReservationsQueryHandler(
        ILogger<GetAllReservationsQueryHandler> logger,
        IReservationsUnitOfWork reservationsUnitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _reservationsUnitOfWork = reservationsUnitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetReservationDetailsResponse>> Handle(GetAllReservationsQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all reservations");
        var reservations = await _reservationsUnitOfWork.Reservations.GetEntities(re => true, cancellationToken);

        if (reservations.Count == 0)
        {
            return [];
        }

        return _mapper.Map<List<GetReservationDetailsResponse>>(reservations);
    }
}