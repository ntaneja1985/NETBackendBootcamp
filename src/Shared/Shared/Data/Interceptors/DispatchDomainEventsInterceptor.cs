using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shared.DDD;

namespace Shared.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator mediator) :SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();   
        return base.SavingChanges(eventData, result);
    }

    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if(context == null) return;

        //Get the aggregates
        var aggregates = context.ChangeTracker
                         .Entries<IAggregate>()
                         .Where(x => x.Entity.DomainEvents.Any())
                         .Select(x => x.Entity);

        //get the domain events
        var domainEvents = aggregates
                           .SelectMany(x => x.DomainEvents)
                           .ToList();

        //Clear the domain events
        aggregates.ToList().ForEach(x=>x.ClearDomainEvents());

        //Publish the domain events
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
                         
    }
}