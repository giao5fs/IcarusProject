using Icarus.Domain.Primitives;

namespace Icarus.Domain.DomainEvents;

public sealed record MemberCreatedDomainEvent(Guid memberId) : IDomainEvent
{
}
