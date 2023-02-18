using Icarus.Application.Abstractions.Messaging;
using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Shared;
using MediatR;

namespace Icarus.Application.Members.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>
    {
    }
}
