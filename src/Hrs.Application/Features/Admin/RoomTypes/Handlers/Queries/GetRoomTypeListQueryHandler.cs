using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.RoomTypes;
using Hrs.Application.Features.Admin.RoomTypes.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.RoomTypes.Handlers.Queries;

public class GetRoomTypeListQueryHandler : IRequestHandler<GetRoomTypeListQuery, List<GetRoomTypeResponse>>
{
    private readonly ILogger<GetRoomTypeListQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;

    public GetRoomTypeListQueryHandler(
        ILogger<GetRoomTypeListQueryHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetRoomTypeResponse>> Handle(GetRoomTypeListQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all room types");

        var roomTypes = await _unitOfWork.RoomTypes.GetEntities(rt => rt.HotelId == query.HotelId, cancellationToken);

        if (roomTypes.Count == 0)
        {
            _logger.LogWarning("There are no room types found!");
            return [];
        }

        return _mapper.Map<List<GetRoomTypeResponse>>(roomTypes);
    }
}