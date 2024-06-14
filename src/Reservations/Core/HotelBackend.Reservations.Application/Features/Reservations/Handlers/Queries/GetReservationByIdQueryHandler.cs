using AutoMapper;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Reservations.Handlers.Queries;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, GetReservationDetailsResponse>
{
    private readonly ILogger<GetReservationByIdQueryHandler> _logger;
    private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
    private readonly IMapper _mapper;

    public GetReservationByIdQueryHandler(
        ILogger<GetReservationByIdQueryHandler> logger,
        IReservationsUnitOfWork reservationsUnitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _reservationsUnitOfWork = reservationsUnitOfWork;
        _mapper = mapper;
    }

    public async Task<GetReservationDetailsResponse> Handle(GetReservationByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reservation={ReservationId}", query.ReservationId);
        var reservation =
            await _reservationsUnitOfWork.Reservations.GetEntity(res => res.Id == query.ReservationId, cancellationToken);

        if (reservation is null)
        {
            _logger.LogError("Reservation with id={ReservationId} does not exist", query.ReservationId);
            throw new NotFoundException($"Reservation with id={query.ReservationId} does not exist");
        }

        return _mapper.Map<GetReservationDetailsResponse>(reservation);
    }
}