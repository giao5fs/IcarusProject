using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Application.Members.Login
{
    internal sealed class LoginCommandHandler
        : ICommandHandler<LoginCommand, string>
    {
        public Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
