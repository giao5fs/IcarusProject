using Icarus.Application.Abstractions.Messaging;

namespace Icarus.Application.Members.RegisterMember
{
    public record RegisterMemberCommand(
        string Email,
        string FirstName,
        string LastName) : ICommand<Guid>;
}
