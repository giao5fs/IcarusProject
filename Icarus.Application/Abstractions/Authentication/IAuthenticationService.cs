using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Shared;
using Icarus.Domain.ValueObjects;

namespace Icarus.Application.Abstractions.Authentication;

public interface IAuthenticationService
{
    Task<Result<TokenResponse>> RegisterAsync(Email email, Password password, FirstName firstName, LastName lastName);
    Task<Result<TokenResponse>> LoginAsync(Email email, Password password);
}
