using MediatR;

namespace Icarus.Domain.Events;
public interface IDomainEvent : INotification
{
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
}
