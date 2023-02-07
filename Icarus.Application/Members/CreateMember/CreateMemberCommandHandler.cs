using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.CreateMember
{
    internal sealed class CreateMemberCommandHandler
        : ICommandHandler<CreateMemberCommand>
    {
        public Task<Result> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {

            return Result.Success();
        }
    }
}
