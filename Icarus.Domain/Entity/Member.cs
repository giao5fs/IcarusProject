using Icarus.Domain.Abtractions;
using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;
using Icarus.Domain.Services;
using Icarus.Domain.ValueObjects;

namespace Icarus.Domain.Entity;

public class Member : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    public Member(Guid id, string email, string passwordHash, string firstName, string lastName) : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        _passwordHash = passwordHash;
    }

    public string Email { get; set; }

    private string _passwordHash;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool Deleted { get; set; }

    public static Member Create(Guid id, Email email, string hashPassword, FirstName firstName, LastName lastName)
    {
        var member = new Member(id, email.Value, hashPassword, firstName.Value, lastName.Value);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }

    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
    {
        return !string.IsNullOrEmpty(password) && passwordHashChecker.IsHashPasswordMatch(_passwordHash, password);
    }
}
