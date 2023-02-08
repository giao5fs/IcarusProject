using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Domain.Primitives
{
    public abstract class AggregateRoot : Entity
    {
        //List<IDomainEvent> _domainEvents = new();
        protected AggregateRoot(Guid id) : base(id)
        {
        }

        //protected void RaiseDomainEvent(IDomainEvent @event)
        //{
        //    _domainEvents.Add(@event);
        //}
    }
}
