using Icarus.Application.Abstractions.Messaging;

namespace Icarus.Application.Members.CreateMember
{
    public record CreateMemberCommand(
        string Email,
        string FirstName,
        string LastName) : ICommand<Guid>;
}
