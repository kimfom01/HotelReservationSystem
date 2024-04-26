using AutoMapper;
using HotelBackend.Application.Contracts.Persistence;
using HotelBackend.Application.Dtos.Reservations;
using HotelBackend.Application.Features.Reservations.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Application.Features.Reservations.Handlers.Queries;

public class GetAllReservationsRequestHandler : IRequestHandler<GetAllReservationsRequest, List<GetReservationDto>>
{
    private readonly ILogger<GetAllReservationsRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllReservationsRequestHandler(
        ILogger<GetAllReservationsRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetReservationDto>> Handle(GetAllReservationsRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all reservations");
        var reservations = await _unitOfWork.Reservations.GetEntities(re => true, cancellationToken);

        if (reservations.Count == 0)
        {
            return [];
        }

        return _mapper.Map<List<GetReservationDto>>(reservations);
    }
}