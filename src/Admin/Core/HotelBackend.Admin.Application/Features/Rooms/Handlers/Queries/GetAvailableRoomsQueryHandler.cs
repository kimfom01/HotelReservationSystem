using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Queries;

public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, List<GetRoomResponse>>
{
    private readonly ILogger<GetAvailableRoomsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetAvailableRoomsQueryHandler(
        ILogger<GetAvailableRoomsQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetRoomResponse>> Handle(GetAvailableRoomsQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting available rooms in hotel={HotelId}", query.HotelId);

        var rooms = await _unitOfWork.Rooms.GetAllAvailableRooms(query.HotelId, cancellationToken);

        if (rooms.Count == 0)
        {
            _logger.LogWarning("No available rooms in hotel={HotelId}", query.HotelId);
            return [];
        }

        return _mapper.Map<List<GetRoomResponse>>(rooms);
    }
}