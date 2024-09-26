using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ReservationService.Application.Contracts.Database;
using ReservationService.Application.Dtos.Reservations;

namespace ReservationService.Application.Features.Reservations.Queries;

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