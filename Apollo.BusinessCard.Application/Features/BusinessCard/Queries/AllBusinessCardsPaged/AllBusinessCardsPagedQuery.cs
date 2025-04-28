using Apollo.BusinessCard.Application.Common.DTOs;
using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Application.Common.Mappings;
using Apollo.BusinessCard.Application.Common.Shared;
using MediatR;

namespace Apollo.BusinessCard.Application.Features.BusinessCard.Queries;

public record AllBusinessCardsPagedQuery(
    string? Name,
    string? DateOfBirth,
    string? Email,
    string? Phone,
    int? GenderId,
    int PageNumber = 1,
    int PageSize = 10
    ) : IRequest<Result<PaginatedList<BusinessCardBrief>>>;

public sealed class AllBusinessCardsPagedHandler : IRequestHandler<AllBusinessCardsPagedQuery, Result<PaginatedList<BusinessCardBrief>>>
{
    private readonly IApplicationDbContext _context;

    public AllBusinessCardsPagedHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PaginatedList<BusinessCardBrief>>> Handle(AllBusinessCardsPagedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.BusinessCard
                .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(x => x.Name.Contains(request.Name.Trim()));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(x => x.Email.Contains(request.Email.Trim()));

        if (!string.IsNullOrWhiteSpace(request.Phone))
            query = query.Where(x => x.Phone.Contains(request.Phone.Trim()));

        if(request.GenderId.HasValue && request.GenderId != null)
            query = query.Where(x => x.GenderId == request.GenderId.Value);
        
        if (!string.IsNullOrWhiteSpace(request.DateOfBirth))
        {
            DateTime dateOfBirth;
            if(DateTime.TryParse(request.DateOfBirth, out dateOfBirth))
            {
                query = query.Where(x => x.DateOfBirth ==  dateOfBirth);
            }
        }


        var result = await query
            .Select(x => new BusinessCardBrief()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                GenderId = x.GenderId,
                GenderName = x.Gender.Name,
                DateOfBirth = x.DateOfBirth,
                Address = x.Address,
                Photo = x.Attachement.Base64File,
                AttachtmentId = x.AttachmentId,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<BusinessCardBrief>>.Success(result);
    }
}
