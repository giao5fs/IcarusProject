using Icarus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Abstractions
{
    public interface IEmailService
    {
        public Task<Member> SendRegisteredMemberAsync(Member member, CancellationToken cancellation);
    }
}
