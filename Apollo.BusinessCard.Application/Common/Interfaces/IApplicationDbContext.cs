using Apollo.BusinessCard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apollo.BusinessCard.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<GenderLookup> GenderLookup { get; }
    DbSet<Domain.Entities.BusinessCard> BusinessCard { get; }
    DbSet<Domain.Entities.BusinessCardAttachment> BusinessCardAttachment { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
