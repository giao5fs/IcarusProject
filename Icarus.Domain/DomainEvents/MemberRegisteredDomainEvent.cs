using Icarus.Domain.Events;

namespace Icarus.Domain.DomainEvents;

public sealed record MemberRegisteredDomainEvent(
    Guid EventId,
    Guid MemberId) : BaseDomainEvent(EventId)
{
}
