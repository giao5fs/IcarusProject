using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;
using Icarus.Domain.ValueObjects;

namespace Icarus.Domain.Entity;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    public Member(Guid id, Email email, FirstName firstName, LastName lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Email Email { get; set; }
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }

    public static Member Create(Guid id, Email email, FirstName firstName, LastName lastName)
    {
        var member = new Member(id, email, firstName, lastName);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }
}
