using Icarus.Domain.Primitives;

namespace Icarus.Domain.Entity;

public sealed class Member : AggregateRoot, IAuditableEntity
{
    public Member(Guid id, string firstName, string lastName, string email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }

    public static Member Create(Guid id, string firstName, string lastName, string email)
    {
        var member = new Member(id, firstName, lastName, email);

        //member.RaiseDomainEvent(new MemberCreatedDomainEvent(member.Id));

        return member;
    }
}
