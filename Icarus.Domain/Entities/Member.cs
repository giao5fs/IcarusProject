using Icarus.Domain.Abtractions;
using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;
using Icarus.Domain.Services;
using Icarus.Domain.ValueObjects;

namespace Icarus.Domain.Entities;

public class Member : AggregateRoot, IAuditableEntity, ISoftDeletableEntity
{
    public Member(Guid id, string email, string password, string firstName, string lastName) : base(id)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? LastModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool Deleted { get; set; }

    public ICollection<Role> Roles { get; set; }

    public static Member Create(Guid id, string email, string hashPassword, string firstName, string lastName)
    {
        var member = new Member(id, email, hashPassword, firstName, lastName);

        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));

        return member;
    }

    public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
    {
        return !string.IsNullOrEmpty(password) && passwordHashChecker.IsHashPasswordMatch(Password, password);
    }
}
