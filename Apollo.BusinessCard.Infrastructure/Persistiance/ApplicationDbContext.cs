using Apollo.BusinessCard.Application.Common.Interfaces;
using Apollo.BusinessCard.Domain.Entities;
using Apollo.BusinessCard.Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Apollo.BusinessCard.Infrastructure.Persistiance;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    public ApplicationDbContext(
        DbContextOptions opt,
        IMediator mediator) 
        : base(opt)
    {
        _mediator = mediator;
    }

    // add Entities here ...
    public virtual DbSet<GenderLookup> GenderLookup { get; set; }
    public virtual DbSet<Apollo.BusinessCard.Domain.Entities.BusinessCard> BusinessCard { get; set; }
    public virtual DbSet<Apollo.BusinessCard.Domain.Entities.BusinessCardAttachment> BusinessCardAttachment { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}
