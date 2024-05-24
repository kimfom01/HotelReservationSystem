using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Queries;

public class GetRoomTypeRequestHandler : IRequestHandler<GetRoomTypeRequest, GetRoomTypeDto>
{
    private readonly ILogger<GetRoomTypeRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomTypeRequestHandler(
        ILogger<GetRoomTypeRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomTypeDto> Handle(GetRoomTypeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting room type with id={RoomTypeId}", request.RoomTypeId);

        var roomType = await _unitOfWork.RoomTypes.GetEntity(rt =>
            rt.Id == request.RoomTypeId &&
            rt.HotelId == request.HotelId);

        if (roomType is null)
        {
            _logger.LogWarning("Room type with id={RoomTypeId} does not exist", request.RoomTypeId);
            throw new NotFoundException($"Room type with id={request.RoomTypeId} does not exist");
        }

        return _mapper.Map<GetRoomTypeDto>(roomType);
    }
}