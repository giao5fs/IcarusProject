using Icarus.Application.Abstractions.Messaging;
using MediatR;

namespace Icarus.Application.Members.Login
{
    public record LoginCommand(string Email) : ICommand
    {
    }
}
