using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Rooms;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Rooms.Handlers.Queries;

public class GetRoomByIdRequestHandler : IRequestHandler<GetRoomByIdRequest, GetRoomDto>
{
    private readonly ILogger<GetRoomByIdRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRoomByIdRequestHandler(
        ILogger<GetRoomByIdRequestHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetRoomDto> Handle(GetRoomByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting room={RoomId}", request.RoomId);

        var room = await _unitOfWork.Rooms.GetEntity(rom =>
            rom.Id == request.RoomId && rom.HotelId == request.HotelId);

        if (room is null)
        {
            _logger.LogWarning("No rooms with id={RoomId}", request.RoomId);
            throw new NotFoundException($"No rooms with id={request.RoomId}");
        }

        return _mapper.Map<GetRoomDto>(room);
    }
}