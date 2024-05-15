using AutoMapper;
using HotelBackend.Reservations.Application.Contracts.Persistence;
using HotelBackend.Reservations.Application.Dtos.Reservations;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Reservations.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Reservations.Handlers.Queries;

public class GetReservationByIdRequestHandler : IRequestHandler<GetReservationByIdRequest, GetReservationDetailsDto>
{
    private readonly ILogger<GetReservationByIdRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetReservationByIdRequestHandler(
        ILogger<GetReservationByIdRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetReservationDetailsDto> Handle(GetReservationByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reservation={reservationId}", request.ReservationId);
        var reservation =
            await _unitOfWork.Reservations.GetEntity(res => res.Id == request.ReservationId, cancellationToken);

        if (reservation is null)
        {
            _logger.LogError("Reservation with id={reservationId} does not exist", request.ReservationId);
            throw new NotFoundException($"Reservation with id={request.ReservationId} does not exist");
        }

        return _mapper.Map<GetReservationDetailsDto>(reservation);
    }
}