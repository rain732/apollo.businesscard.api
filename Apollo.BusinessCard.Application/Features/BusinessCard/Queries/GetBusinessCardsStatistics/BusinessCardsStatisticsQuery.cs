using Apollo.BusinessCard.Application.Common.DTOs;
using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Application.Common.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apollo.BusinessCard.Application.Features.BusinessCard.Queries;

public record BusinessCardsStatisticsQuery : IRequest<Result<BusinessCardsStatisticsBrief>>;

public sealed class BusinessCardsStatisticsHandler : IRequestHandler<BusinessCardsStatisticsQuery, Result<BusinessCardsStatisticsBrief>>
{
    private readonly IApplicationDbContext _context;

    public BusinessCardsStatisticsHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<BusinessCardsStatisticsBrief>> Handle(BusinessCardsStatisticsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.BusinessCard.LongCountAsync(cancellationToken);

        return Result<BusinessCardsStatisticsBrief>.Success(new BusinessCardsStatisticsBrief() { Total = result });
    }
}
