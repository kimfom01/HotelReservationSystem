using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.RoomTypes;
using HotelBackend.Admin.Application.Features.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.RoomTypes.Handlers.Queries;

public class GetRoomTypeListQueryHandler : IRequestHandler<GetRoomTypeListQuery, List<GetRoomTypeResponse>>
{
    private readonly ILogger<GetRoomTypeListQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomTypeListQueryHandler(
        ILogger<GetRoomTypeListQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetRoomTypeResponse>> Handle(GetRoomTypeListQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all room types");

        var roomTypes = await _unitOfWork.RoomTypes.GetEntities(rt => rt.HotelId == query.HotelId);

        if (roomTypes.Count == 0)
        {
            _logger.LogWarning("There are no room types found!");
            return [];
        }

        return _mapper.Map<List<GetRoomTypeResponse>>(roomTypes);
    }
}