using AutoMapper;
using HotelBackend.Reservations.Application.Contracts.Database;
using HotelBackend.Reservations.Application.Dtos.Admin.Rooms;
using HotelBackend.Reservations.Application.Exceptions;
using HotelBackend.Reservations.Application.Features.Admin.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Reservations.Application.Features.Admin.Rooms.Handlers.Queries;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, GetRoomResponse>
{
    private readonly ILogger<GetRoomByIdQueryHandler> _logger;
    private readonly IAdminUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoomByIdQueryHandler(
        ILogger<GetRoomByIdQueryHandler> logger,
        IAdminUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetRoomResponse> Handle(GetRoomByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting room={RoomId}", query.RoomId);

        var room = await _unitOfWork.Rooms.GetEntity(rom =>
            rom.Id == query.RoomId && rom.HotelId == query.HotelId, cancellationToken);

        if (room is null)
        {
            _logger.LogWarning("No rooms with id={RoomId}", query.RoomId);
            throw new NotFoundException($"No rooms with id={query.RoomId}");
        }

        return _mapper.Map<GetRoomResponse>(room);
    }
}