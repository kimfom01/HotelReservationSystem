using Admin.Application.Contracts.Database;
using Admin.Application.Dtos.Admin.RoomTypes;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Features.Admin.RoomTypes.Queries;

public class GetRoomTypeListQueryHandler : IRequestHandler<GetRoomTypeListQuery, IReadOnlyList<GetRoomTypeResponse>>
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

    public async Task<IReadOnlyList<GetRoomTypeResponse>> Handle(GetRoomTypeListQuery query,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all room types");

        var roomTypes = await _unitOfWork.RoomTypes.GetRoomTypes(query.HotelId, cancellationToken);

        if (roomTypes.Count == 0)
        {
            _logger.LogWarning("There are no room types found!");
            return [];
        }

        return _mapper.Map<IReadOnlyList<GetRoomTypeResponse>>(roomTypes);
    }
}