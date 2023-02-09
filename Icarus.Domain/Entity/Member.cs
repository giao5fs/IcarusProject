using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;

namespace Icarus.Domain.Entity;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    public Member(Guid id, string email, string firstName, string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }

    public static Member Create(Guid id, string email, string firstName, string lastName)
    {
        var member = new Member(id, email, firstName, lastName);

        member.RaiseDomainEvent(new MemberCreatedDomainEvent(member.Id));

        return member;
    }
}
