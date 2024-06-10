using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Exceptions;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Queries;

public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, GetRoomTypeResponse>
{
    private readonly ILogger<GetRoomTypeQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomTypeQueryHandler(
        ILogger<GetRoomTypeQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomTypeResponse> Handle(GetRoomTypeQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting room type with id={RoomTypeId}", query.RoomTypeId);

        var roomType = await _unitOfWork.RoomTypes.GetEntity(rt =>
            rt.Id == query.RoomTypeId &&
            rt.HotelId == query.HotelId);

        if (roomType is null)
        {
            _logger.LogWarning("Room type with id={RoomTypeId} does not exist", query.RoomTypeId);
            throw new NotFoundException($"Room type with id={query.RoomTypeId} does not exist");
        }

        return _mapper.Map<GetRoomTypeResponse>(roomType);
    }
}