using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Queries;

public class GetRoomTypeListRequestHandler : IRequestHandler<GetRoomTypeListRequest, List<GetRoomTypeDto>>
{
    private readonly ILogger<GetRoomTypeListRequestHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomTypeListRequestHandler(
        ILogger<GetRoomTypeListRequestHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetRoomTypeDto>> Handle(GetRoomTypeListRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all room types");

        var roomTypes = await _unitOfWork.RoomTypes.GetEntities(rt => rt.HotelId == request.HotelId);

        if (roomTypes.Count == 0)
        {
            _logger.LogWarning("There are no room types found!");
            return [];
        }

        return _mapper.Map<List<GetRoomTypeDto>>(roomTypes);
    }
}