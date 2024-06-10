using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Queries;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, GetRoomResponse>
{
    private readonly ILogger<GetRoomByIdQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoomByIdQueryHandler(
        ILogger<GetRoomByIdQueryHandler> logger,
        IUnitOfWork unitOfWork,
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
            rom.Id == query.RoomId && rom.HotelId == query.HotelId);

        if (room is null)
        {
            _logger.LogWarning("No rooms with id={RoomId}", query.RoomId);
            throw new NotFoundException($"No rooms with id={query.RoomId}");
        }

        return _mapper.Map<GetRoomResponse>(room);
    }
}