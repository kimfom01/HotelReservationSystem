using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Features.Admin.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Rooms.Handlers.Queries;

public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, List<GetRoomResponse>>
{
    private readonly ILogger<GetAvailableRoomsQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;

    public GetAvailableRoomsQueryHandler(
        ILogger<GetAvailableRoomsQueryHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork)
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