using Icarus.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.Queries
{
    public sealed record GetMemberByIdQuery(Guid memberId): IQuery<MemberResponse>; 

}
