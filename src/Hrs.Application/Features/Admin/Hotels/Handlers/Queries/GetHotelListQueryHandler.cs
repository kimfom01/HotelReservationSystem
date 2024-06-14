using AutoMapper;
using Hrs.Application.Contracts.Database;
using Hrs.Application.Dtos.Admin.Hotels;
using Hrs.Application.Features.Admin.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrs.Application.Features.Admin.Hotels.Handlers.Queries;

public class GetHotelListQueryHandler : IRequestHandler<GetHotelListQuery, List<GetHotelResponse>>
{
    private readonly ILogger<GetHotelListQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminUnitOfWork _unitOfWork;

    public GetHotelListQueryHandler(
        ILogger<GetHotelListQueryHandler> logger,
        IMapper mapper,
        IAdminUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetHotelResponse>> Handle(GetHotelListQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all hotels");
        var hotels = await _unitOfWork.Hotels.GetEntities(hot => true, cancellationToken);

        if (hotels.Count == 0)
        {
            _logger.LogWarning("There are no hotels found or registered!");
            return [];
        }

        return _mapper.Map<List<GetHotelResponse>>(hotels);
    }
}