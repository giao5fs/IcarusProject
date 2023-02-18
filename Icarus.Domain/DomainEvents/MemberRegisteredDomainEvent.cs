using Icarus.Domain.Events;

namespace Icarus.Domain.DomainEvents;

public sealed record MemberRegisteredDomainEvent(Guid memberId) : IDomainEvent
{
}
