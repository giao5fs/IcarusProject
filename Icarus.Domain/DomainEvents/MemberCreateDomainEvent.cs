using Icarus.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.DomainEvents
{
    public sealed record MemberCreateDomainEvent(Guid id, ) : IDomainEvent
    {
    }
}
