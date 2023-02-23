using Icarus.Domain.Events;
using Icarus.Persistence;
using Icarus.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Icarus.Infrastructure.Idempotence;
public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly IcarusDbContext _context;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, IcarusDbContext context)
    {
        _decorated = decorated;
        _context = context;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        if (await _context.OutboxMessageConsumers
            .AnyAsync(outboxMessageConsumer =>
            outboxMessageConsumer.Id == notification.EventId &&
            outboxMessageConsumer.Name == consumer, cancellationToken))
        {
            return;
        }
        await _decorated.Handle(notification, cancellationToken);

        _context.OutboxMessageConsumers.Add(new OutboxMessageConsumer
        {
            Id = notification.EventId,
            Name = consumer
        });

        await _context.SaveChangeAsync(cancellationToken);
    }
}
