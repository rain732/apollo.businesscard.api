using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Apollo.BusinessCard.Infrastructure.Common;

public static class MediatorExtentions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        
    }
}
