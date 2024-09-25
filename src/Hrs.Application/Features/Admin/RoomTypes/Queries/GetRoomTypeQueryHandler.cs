using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.RoomTypes;
using Hrs.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.RoomTypes.Queries;

public class GetRoomTypeQueryHandler : IRequestHandler<GetRoomTypeQuery, GetRoomTypeResponse>
{
    private readonly ILogger<GetRoomTypeQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;

    public GetRoomTypeQueryHandler(
        ILogger<GetRoomTypeQueryHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetRoomTypeResponse> Handle(GetRoomTypeQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting room type with id={RoomTypeId}", query.RoomTypeId);

        var roomType = await _unitOfWork.RoomTypes.GetRoomType(query.RoomTypeId, query.HotelId, cancellationToken);

        if (roomType is null)
        {
            _logger.LogWarning("Room type with id={RoomTypeId} does not exist", query.RoomTypeId);
            throw new NotFoundException($"Room type with id={query.RoomTypeId} does not exist");
        }

        return _mapper.Map<GetRoomTypeResponse>(roomType);
    }
}