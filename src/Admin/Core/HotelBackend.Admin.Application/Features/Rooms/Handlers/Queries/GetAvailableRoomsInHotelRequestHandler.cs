using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Queries;

public class GetAvailableRoomsInHotelRequestHandler : IRequestHandler<GetAvailableRoomsInHotelRequest, List<RoomDto>>
{
    private readonly ILogger<GetAvailableRoomsInHotelRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableRoomsInHotelRequestHandler(
        ILogger<GetAvailableRoomsInHotelRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<RoomDto>> Handle(GetAvailableRoomsInHotelRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting available rooms in hotel={HotelId}", request.HotelId);

        var rooms = await _unitOfWork.Rooms.GetAllAvailableRooms(request.HotelId, cancellationToken);

        if (rooms.Count == 0)
        {
            _logger.LogWarning("No available rooms in hotel={HotelId}", request.HotelId);
            return [];
        }

        return _mapper.Map<List<RoomDto>>(rooms);
    }
}