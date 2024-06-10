using AutoMapper;
using HotelBackend.Admin.Application.Contracts.Infrastructure.Database;
using HotelBackend.Admin.Application.Dtos.Hotels;
using HotelBackend.Admin.Application.Features.Hotels.Requests.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelBackend.Admin.Application.Features.Hotels.Handlers.Queries;

public class GetHotelListQueryHandler : IRequestHandler<GetHotelListQuery, List<GetHotelResponse>>
{
    private readonly ILogger<GetHotelListQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHotelListQueryHandler(
        ILogger<GetHotelListQueryHandler> logger,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetHotelResponse>> Handle(GetHotelListQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all hotels");
        var hotels = await _unitOfWork.Hotels.GetEntities(hot => true);

        if (hotels.Count == 0)
        {
            _logger.LogWarning("There are no hotels found or registered!");
            return [];
        }

        return _mapper.Map<List<GetHotelResponse>>(hotels);
    }
}