using MediatR;

namespace Icarus.Domain.Events;

/// <summary>
/// Represents a marker interface for a domain event.
/// </summary>
public interface IDomainEvent : INotification
{
}
