using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Rooms;
using Hrs.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Rooms.Queries;

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