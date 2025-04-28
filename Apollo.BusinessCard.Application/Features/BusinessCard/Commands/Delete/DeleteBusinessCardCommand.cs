using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Application.Common.Shared;
using Apollo.BusinessCard.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apollo.BusinessCard.Application.Features.BusinessCard.Commands;

public record DeleteBusinessCardCommand(int Id) : IRequest<Result<bool>>;

public sealed class DeleteBusinessCardHandler : IRequestHandler<DeleteBusinessCardCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteBusinessCardHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteBusinessCardCommand request, CancellationToken cancellationToken)
    {
        var card = await _context.BusinessCard
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (card == null) return Result<bool>.Failure(Localization.ERROR_DELETE_BUSINESSCARD);

        card.IsDeleted = true;
        card.LastModified = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);
        return Result<bool>.Success(true);
    }
}
