using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;
using Icarus.Domain.ValueObjects;

namespace Icarus.Domain.Entity;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    public Member(Guid id, string email, string firstName, string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }

    public static Member Create(Guid id, Email email, FirstName firstName, LastName lastName)
    {
        var member = new Member(id, email.Value, firstName.Value, lastName.Value);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }
}
