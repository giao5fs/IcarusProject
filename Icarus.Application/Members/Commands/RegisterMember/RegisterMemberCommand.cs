using Icarus.Application.Abstractions.Messaging;
using Icarus.Domain.Shared;

namespace Icarus.Application.Members.RegisterMember
{
    public record RegisterMemberCommand(
        string Email,
        string Password,
        string ConfirmPassword,
        string FirstName,
        string LastName
        ) : ICommand<Result<Guid>>;
}
