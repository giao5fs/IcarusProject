using Icarus.Domain.DomainEvents;
using Icarus.Domain.Primitives;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Entity
{
    public sealed class Member : AggregateRoot
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

        public static Member Create(Guid id, string firstName, string lastName, string email)
        {
            var member = new Member(id, firstName, lastName, email);

            member.RaiseDomainEvent(new MemberCreatedDomainEvent(member.Id));

            return member;
        }
    }
}
