using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.Queries
{
    public sealed record MemberResponse(Guid Id, string Email);
}
